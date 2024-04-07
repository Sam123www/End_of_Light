using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyGhost : Enemy
{
    public float Speed;
    public enum Status { idle, track, avoid };
    public Status status;
    void Start()
    {
        status = Status.idle;
    }
    protected override void Update()
    {
        base.Update();
    }
    void FixedUpdate()
    {
        switch (status)
        {
            case Status.idle:
                if (playerCheck_circle)
                    status = Status.track;
                break;
            case Status.track:
                float x = playerTransform.position.x - transform.position.x;
                float y = playerTransform.position.y - transform.position.y;
                float r = math.sqrt(x * x + y * y);
                x /= r;
                y /= r;
                transform.position = new Vector2(transform.position.x + x * Speed * 0.001f, transform.position.y + y * Speed * 0.001f);
                if(!playerCheck_circle) status = Status.avoid;
                break;
            case Status.avoid:
                x = playerTransform.position.x - transform.position.x;
                y = playerTransform.position.y - transform.position.y;
                r = math.sqrt(x * x + y * y);
                x /= r;
                y /= r;
                transform.position = new Vector2(transform.position.x + x * Speed * (-0.003f), transform.position.y + y * Speed * (-0.003f));
                break;

        }
    }
}
