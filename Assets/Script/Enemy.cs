using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public bool playerCheck_L, playerCheck_R;
    public float range_radius, range_x, range_y, offset_y, hp, damage;
    public Collider2D playerCheck_circle;
    public LayerMask playerMask;
    public Transform playerTransform;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        PhysicsCheck();
    }
    protected void PhysicsCheck()
    {
        playerCheck_circle = Physics2D.OverlapCircle(transform.position, range_radius, playerMask);
        Vector2 pointA = new Vector2(transform.position.x - range_x, transform.position.y + range_y + offset_y);
        Vector2 pointB = new Vector2(transform.position.x + range_x, transform.position.y + range_y + offset_y);
        Vector2 pointO = new Vector2(transform.position.x, transform.position.y - range_y + offset_y);
        playerCheck_L = Physics2D.OverlapArea(pointA, pointO, playerMask);
        playerCheck_R = Physics2D.OverlapArea(pointB, pointO, playerMask);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range_radius);
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + offset_y, 0), new Vector3(range_x, range_y, 1));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && damage > 0)
        {
            if (collision.transform.position.x > transform.position.x)
            {
                float[] data = { damage, 0 };
                collision.gameObject.SendMessage("reduceHp", data);
            }
            else
            {
                float[] data = { damage, 1 };
                collision.gameObject.SendMessage("reduceHp", data);
            }
            Debug.Log("hurt");
        }
    }
}
