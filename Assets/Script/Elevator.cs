using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Elevator : MonoBehaviour
{
    public float speed;
    public Transform upPoint, downPoint;
    bool toUp = true;
    public GameObject lightObj;
    Rigidbody2D rb;
    public void Enable()
    {
        rb = GetComponent<Rigidbody2D>();
        if (toUp)
        {
            rb.velocity = Vector2.up * speed;
        }
        else
        {
            rb.velocity = Vector2.down * speed;
        }
    }
    private void FixedUpdate()
    {
        if (toUp && transform.position.y >= upPoint.position.y)
        {
            toUp = false;
            rb.velocity = Vector2.zero;
            lightObj.SendMessage("Disable");
        }
        if(!toUp && transform.position.y <= downPoint.position.y)
        {
            toUp = true;
            rb.velocity = Vector2.zero;
            lightObj.SendMessage("Disable");
        }
    }
}
