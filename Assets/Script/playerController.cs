using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public static playerController PlayerController;
    Rigidbody2D rb;
    [Header("Light")]
    bool takingOutLight, turnOffLight, isLighting;
    public GameObject Light;
    [Header("collision")]
    public bool onGround, onGroundEnter;
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
    const string anim_jump = "Jump";
    const string anim_fall = "Fall";
    const string anim_takeOutLight = "TakeOutLight";
    const string anim_turnOffLight = "TurnOffLight";
    const string anim_attack = "Attack";
    public bool isFalling, isAttacking;
    private void Awake()
    {
        PlayerController = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpTimes = jumpMaxTimes;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !takingOutLight && !isLighting)
        {
            takingOutLight = true;
            isLighting = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !turnOffLight && isLighting)
        {
            turnOffLight = true;
            isLighting = false;
        }
        Animation();
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
        if(currentState == newState || isAttacking) return;
        anim.Play(newState);
        currentState = newState;
    }
    void Animation()
    {
        if (takingOutLight)
        {
            ChangeAnimationState(anim_takeOutLight);
        }
        else if (turnOffLight)
        {
            ChangeAnimationState(anim_turnOffLight);
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("attack!");
                ChangeAnimationState(anim_attack);
                isAttacking = true;
            }
            else if (onGround)
            {
                if (Mathf.Abs(rb.velocity.x) > 0.1)
                    ChangeAnimationState(anim_run);
                else
                    ChangeAnimationState(anim_idle);
            }
            else
            {
                if (rb.velocity.y < -0.1)
                    ChangeAnimationState(anim_fall);
                else if (rb.velocity.y > 0.1)
                    ChangeAnimationState(anim_jump);
            }
        }
    }

    public void takeOutLightEnd()
    {
        takingOutLight = false;
    }
    public void turnOffLightEnd()
    {
        turnOffLight = false;
    }
    public void switchLight(string state)
    {
        if (state == "on") Light.SetActive(true);
        else Light.SetActive(false);
    }
    void Movement()
    {
        float hori = Input.GetAxis("Horizontal");
        if (Mathf.Abs(hori) > 0)
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
        if (onGround && !onGroundEnter)
        {
            isFalling = false;
            jumpTimes = jumpMaxTimes;
            onGroundEnter = true;
        }
        else if (!onGround) 
        {
            onGroundEnter = false;
        }
        jumpHold = Input.GetButton("Jump");
        if (Input.GetButtonDown("Jump") && jumpTimes > 0)
        {
            jumpPressing = true;
        }
    }
    void JumpUp()
    {
        if(rb.velocity.y < -0.1f && !isFalling)
        {
            isFalling = true;
        }
        if (jumpPressing)
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
