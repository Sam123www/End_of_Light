using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
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
                if (playerCheck_circle != null)
                {
                    playerTransform = playerCheck_circle.transform;
                    transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, Speed*0.001f);
                    if (playerTransform.position.x > transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    if (playerTransform.position.x < transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                break;
            case Status.avoid:
                Invoke("Track", 2f);
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, -Speed*0.003f);
                if (playerTransform.position.x < transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                if (playerTransform.position.x > transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                break;
        }
    }
    public void Avoid()
    {
        status = Status.avoid;
    }
    void Track()
    {
        status = Status.track;
    }
}
