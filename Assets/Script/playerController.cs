using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    [Header("collision")]
    public bool onGround;
    public float rayDis;
    public LayerMask groundMask;
    [Header("Movement")]
    public float Speed;
    [Header("Jump")]
    bool jumpPressing, isJump, jumpHold;
    public int jumpTimes;
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
        PhysicsCheck();
        JumpCheck();
    }
    void FixedUpdate()
    {
        JumpUp();
        Movement();
    }
    void Movement()
    {
        float hori = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2 (hori*Speed, rb.velocity.y);
    }
    void PhysicsCheck()
    {
        Debug.DrawRay(transform.position, Vector2.down*rayDis, Color.red);
        onGround = Physics2D.Raycast(transform.position, Vector2.down, rayDis, groundMask);
    }
    void JumpCheck()
    {
        if (onGround)
        {
            jumpTimes = jumpMaxTimes;
        }
        jumpHold = Input.GetButton("Jump");
        if (Input.GetButtonDown("Jump") && jumpTimes > 0)
        {
            jumpPressing = true;
        }
    }
    void JumpUp()
    {
        if(jumpPressing)
        {
            isJump = true;
            jumpTime = Time.time + 0.2f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpPressing = false;
            jumpTimes--;
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
