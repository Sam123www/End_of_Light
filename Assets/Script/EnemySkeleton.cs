using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySekelton : Enemy
{
    public float Speed;
    public enum Status { idle, track, avoid };
    public Status status;
    [Header("Animation")]
    const string anim_idle = "Idle";
    const string anim_run = "Run";
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
            case Status.avoid:
                break;
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
    void Animation()
    {
        if(Mathf.Abs(rb.velocity.x) > 0.1)
        {
            ChangeAnimationState(anim_run);
        }
        else
        {
            ChangeAnimationState(anim_idle);
        }
    }
}
