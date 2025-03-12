using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering.Universal;

public class EnemySekelton : Enemy
{
    public Light2D sk_light;
    public float li_intensity;
    public bool isRight, rotating, finding;
    public float Speed, hurtSpeed_x, hurtSpeed_y;
    public enum Status {idle, track, dead, hurt, found};
    public Status status;
    [Header("Animation")]
    const string anim_idle = "Idle";
    const string anim_run = "Run";
    const string anim_death = "Death";
    const string anim_found = "Found";
    string currentState;
    void Start()
    {
        status = Status.idle;
        StartCoroutine(wallChecking());
    }
    protected override void Update()
    {
        base.Update();
        Animation();
    }
    IEnumerator wallChecking()
    {
        wallCheck = Physics2D.OverlapBox(transform.position, new Vector2(range_wall, 1), 0, groundMask);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(wallChecking());
    }
    private void FixedUpdate()
    {
        switch (status)
        {
            case Status.idle:
                sk_light.intensity = 0;
                if (wallCheck)
                {
                    wallCheck = false;
                    isRight = !isRight;
                }
                if (isRight)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    rb.velocity = new Vector2(Speed, rb.velocity.y);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    rb.velocity = new Vector2(-Speed, rb.velocity.y);
                }
                if (playerCheck_circle && !Physics2D.Linecast(transform.position, playerCheck_circle.transform.position, groundMask))
                    status = Status.found;
                break;
            case Status.found:
                if (!finding)
                {
                    StartCoroutine(transition());
                    finding = true;
                }
                break;
            case Status.track:
                if (playerCheck_circle && !Physics2D.Linecast(transform.position, playerCheck_circle.transform.position, groundMask))
                {
                    changeDir();
                    if (isRight)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        rb.velocity = new Vector2(2*Speed, rb.velocity.y);
                        if(playerCheck_circle.transform.position.x < transform.position.x && !rotating)
                        {
                            rotating = true;
                            StartCoroutine(changeDir());
                        }
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        rb.velocity = new Vector2(-2*Speed, rb.velocity.y);
                        if (playerCheck_circle.transform.position.x > transform.position.x && !rotating)
                        {
                            rotating = true;
                            StartCoroutine(changeDir());
                        }
                    }
                }
                else
                {
                    status = Status.idle;
                }
                break;
            case Status.hurt:
                StartCoroutine(hurting());
                break;
            case Status.dead:
                StartCoroutine(goDead());
                break;
        }
    }
    IEnumerator transition()
    {
        if (playerCheck_circle.transform.position.x < transform.position.x)
        {
            isRight = false;
        }
        else
        {
            isRight = true;
        }
        sk_light.intensity = li_intensity;
        ChangeAnimationState(anim_found);
        yield return new WaitForSeconds(0.5f);
        finding = false;
        status = Status.track;
    }
    IEnumerator changeDir()
    {
        yield return new WaitForSeconds(0.5f);
        isRight = !isRight;
        rotating = false;
    }
    IEnumerator hurting()
    {
        yield return new WaitForSeconds(0.5f);
        status = Status.idle;
    }
    IEnumerator goDead()
    {
        damage = 0;
        sk_light.intensity = 0;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
    void Animation()
    {
        if (status != Status.idle && status != Status.track) return;
        if(Mathf.Abs(rb.velocity.x) > 0.1)
        {
            ChangeAnimationState(anim_run);
        }
        else
        {
            ChangeAnimationState(anim_idle);
        }
    }
    public void onDamage(float[] damage)
    {
        status = Status.hurt;
        if (damage[1] == 0)
        {
            rb.velocity = new Vector2(hurtSpeed_x, hurtSpeed_y);
        }
        else
        {
            rb.velocity = new Vector2(-hurtSpeed_x, hurtSpeed_y);
        }
        hp -= damage[0];
        if (hp <= 0)
        {
            ChangeAnimationState(anim_death);
            status = Status.dead;
        }
        Debug.Log(hp);
    }
}
