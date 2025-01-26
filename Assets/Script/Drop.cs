using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public float damage, stop, offset_stop;
    public Vector2 pos, size, old_pos, offset_center;
    Rigidbody2D rb;
    public LayerMask playerMask;
    void Start()
    {
        old_pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    void Update()
    {
        if (Physics2D.OverlapBox(old_pos + pos + offset_center, size, 0, playerMask))
        {
            Debug.Log("triggering");
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        if(transform.position.y <= old_pos.y + stop)
        {
            rb.bodyType = RigidbodyType2D.Static;
            transform.position = new Vector2(transform.position.x, old_pos.y + stop);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(new Vector2(transform.position.x + pos.x + offset_center.x, transform.position.y + pos.y + offset_center.y), size);
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y + stop + offset_stop), new Vector2(1, 0.1f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
        }
    }
}
