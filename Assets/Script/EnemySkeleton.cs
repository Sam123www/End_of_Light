using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class EnemySekelton : Enemy
{
    public bool isRight;
    public float Speed, hurtSpeed_x, hurtSpeed_y;
    public enum Status { idle, walk, track, dead, hurt };
    public Status status;
    [Header("Animation")]
    const string anim_idle = "Idle";
    const string anim_run = "Run";
    const string anim_death = "Death";
    string currentState;
    void Start()
    {
        status = Status.idle;
    }
    protected override void Update()
    {
        base.Update();
        Animation();
    }
    private void FixedUpdate()
    {
        switch(status)
        {
            case Status.idle:
                if (playerCheck_L || playerCheck_R)
                    status = Status.track;
                break;
            case Status.track:
                if (playerCheck_L)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    rb.velocity = new Vector2(-Speed, rb.velocity.y);
                }
                else if (playerCheck_R)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    rb.velocity = new Vector2(Speed, rb.velocity.y) ;
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
    IEnumerator hurting()
    {
        yield return new WaitForSeconds(0.5f);
        status = Status.idle;
    }
    IEnumerator goDead()
    {
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
