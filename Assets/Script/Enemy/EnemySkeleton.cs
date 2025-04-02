using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemySekelton : Enemy
{
    public Light2D sk_light;
    public float li_intensity;
    public bool isRight, rotating, changing;
    public float Speed, hurtSpeed_x, hurtSpeed_y;
    public enum Status {idle, walk, track, dead, hurt};
    public Status status;
    [Header("Animation")]
    const string anim_idle = "Idle";
    const string anim_run = "Run";
    const string anim_death = "Death";
    string currentState;
    void Start()
    {
        status = Status.walk;
        StartCoroutine(wallChecking());
    }
    protected override void Update()
    {
        base.Update();
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
                break;
            case Status.walk:
                ChangeAnimationState(anim_run);
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
                {
                    sk_light.intensity = li_intensity;
                    if (playerCheck_circle.transform.position.x < transform.position.x)
                    {
                        isRight = false;
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        isRight = true;
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    status = Status.idle;
                    ChangeAnimationState(anim_idle);
                    StartCoroutine(ChangeStatus(0.5f, Status.track));
                }
                break;
            case Status.track:
                ChangeAnimationState(anim_run);
                if (playerCheck_circle && !Physics2D.Linecast(transform.position, playerCheck_circle.transform.position, groundMask))
                {
                    anim.speed = 2;
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
                    anim.speed = 1;
                    StartCoroutine(ChangeStatus(0.5f, Status.walk));
                }
                break;
            case Status.hurt:
                status = Status.idle;
                StartCoroutine(ChangeStatus(0.5f, Status.track));
                break;
            case Status.dead:
                break;
        }
    }
    IEnumerator ChangeStatus(float waitTime, Status sta)
    {
        if (changing) yield return null;
        changing = true;
        yield return new WaitForSeconds(waitTime);
        status = sta;
        changing = false;
    }
    IEnumerator changeDir()
    {
        yield return new WaitForSeconds(0.5f);
        isRight = !isRight;
        rotating = false;
    }
    IEnumerator toDead()
    {
        status = Status.dead;
        ChangeAnimationState(anim_death);
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
    public void onDamage(float[] damage)
    {
        if(status == Status.dead) return;
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
            gameObject.layer = LayerMask.NameToLayer("None");
            StopAllCoroutines();
            StartCoroutine(toDead());
        }
    }
}
