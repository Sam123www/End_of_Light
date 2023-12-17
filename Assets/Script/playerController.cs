using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb;
    
    [Header("collision")]
    public bool onGround;
    public float check_x_size, check_y_size, check_offset;
    public LayerMask groundMask;
    [Header("Movement")]
    public float Speed;
    [Header("Jump")]
    bool jumpPressing, isJump, jumpHold;
    public int jumpTimes;
    float jumpTime;
    public int jumpMaxTimes;
    public float jumpForce, jumpForceHold;
    [Header("Animation")]
    Animator anim;
    string currentState;
    const string anim_idle = "Idle";
    const string anim_run = "Run";
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
    void ChangeAnimationState(string newState)
    {
        if(currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
    void Movement()
    {
        float hori = Input.GetAxis("Horizontal");
        if (Mathf.Abs(hori) > 0)
        {
            ChangeAnimationState(anim_run);
        }
        else
        {
            ChangeAnimationState(anim_idle);
        }
        if(hori > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(hori < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        rb.velocity = new Vector2 (hori*Speed, rb.velocity.y);
    }
    void PhysicsCheck()
    {
        Vector2 offset = new Vector2(0, -check_offset);
        onGround = Physics2D.OverlapBox((Vector2)transform.position + offset, new Vector2(check_x_size, check_y_size), 0, groundMask);
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 offset = new Vector2(0, -check_offset);
        Gizmos.DrawWireCube((Vector2)transform.position + offset, new Vector2(check_x_size, check_y_size));
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
