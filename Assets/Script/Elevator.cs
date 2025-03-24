using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Elevator : MonoBehaviour
{
    public float speed;
    public Transform endPoint;
    Rigidbody2D rb;
    public void Enable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
    }
    private void FixedUpdate()
    {
        if (transform.position.y >= endPoint.position.y)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
