using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    [Header("Movement")]
    public float Speed;
    [Header("Jump")]
    bool jumpPressing, isJump, jumpHold;
    int jumpTimes;
    float jumpTime;
    public int jumpMaxTimes;
    public float jumpForce, jumpForceHold;
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
        jumpHold = Input.GetButton("Jump");
        if (Input.GetButtonDown("Jump") && jumpTimes > 0)
        {
            jumpTimes--;
            jumpPressing = true;
        }
    }
    void JumpUp()
    {
        if(jumpPressing)
        {
            isJump = true;
            jumpTime = Time.time + 0.1f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpPressing = false;
        }
        else if (isJump)
        {
            if (jumpHold)
            {
                rb.AddForce(new Vector2(0, jumpForceHold), ForceMode2D.Impulse);
            }
            if (jumpTime < Time.time || Input.GetButtonUp("Jump"))
            {
                isJump = false;
            }
        }
    }
}
