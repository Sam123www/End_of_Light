using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    [Header("Movement")]
    public float Speed;
    [Header("Jump")]
    bool isJump;
    int jumpTimes;
    public int jumpMaxTimes;
    public float jumpForce;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpTimes = jumpMaxTimes;
    }
    void Update()
    {
        Jump();
    }
    void FixedUpdate()
    {
        JumpUp();
        Movement();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            jumpTimes = jumpMaxTimes;
        }
    }
    void Movement()
    {
        float hori = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2 (hori*Speed, rb.velocity.y);
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpTimes > 0)
        {
            jumpTimes--;
            isJump = true;
        }
    }
    void JumpUp()
    {
        if(isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJump = false;
        }
    }
}
