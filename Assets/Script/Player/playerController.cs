using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb;
    public static playerController instance;
    public LayerMask playerLayer, groundLayer, oneWayGroundLayer;
    [Header("Light")]
    public bool takingOutLight, turnOffLight, isLighting;
    public GameObject Light;
    [Header("collision")]
    Collider2D onOneWayGroundTop, onOneWayGroundBottom;
    public bool onGround, onGroundEnter;
    public float check_x_size, check_y_size, check_offset_down, check_offset_top;
    [Header("Movement")]
    public float Speed;
    public bool canMove = true;
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
    public bool isFalling, isAttack, canChangeAnim = true;
    [Header("Attack")]
    public float attack_cd, attackTime, attackTimer;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpTimes = jumpMaxTimes;
        transform.position = GameManager.instance.playerPosition;
    }
    void Update()
    {
        if (Input.GetButtonDown("Light") && !takingOutLight && !isLighting && float.Parse(player_UI.playerUI.lightnum.text) > 0)
        {
            takingOutLight = true;
            isLighting = true;
        }
        else if (Input.GetButtonDown("Light") && !turnOffLight && isLighting)
        {
            turnOffLight = true;
            isLighting = false;
        }
        Animation();
        PhysicsCheck();
        JumpCheck();
        oneWayGroundCheck();
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            JumpUp();
            Movement();
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState || !canChangeAnim) return;
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
            if (Input.GetButtonDown("Fire1") && attackTimer < Time.time)
            {
                ChangeAnimationState(anim_attack);
                canMove = false;
                canChangeAnim = false;
                attackTimer = Time.time + attack_cd;
                StartCoroutine(AttackEnd());
            }
            else if (onGround)
            {
                float hori = Input.GetAxis("Horizontal");
                if (Mathf.Abs(hori) > 0)
                    ChangeAnimationState(anim_run);
                else
                    ChangeAnimationState(anim_idle);
            }
            else
            {
                if (rb.velocity.y < -0.1)
                    ChangeAnimationState(anim_fall);
                else if (rb.velocity.y > 0.1 && Input.GetButton("Jump"))
                    ChangeAnimationState(anim_jump);
            }
        }
    }
    IEnumerator AttackEnd()
    {
        yield return new WaitForSeconds(attackTime);
        canMove = true;
        canChangeAnim = true;
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
        Vector2 offset = new Vector2(0, -check_offset_down);
        onOneWayGroundBottom = Physics2D.OverlapBox((Vector2)transform.position + offset, new Vector2(check_x_size, check_y_size), 0, oneWayGroundLayer);
        onGround = Physics2D.OverlapBox((Vector2)transform.position + offset, new Vector2(check_x_size, check_y_size), 0, groundLayer);
        onGround = onGround || onOneWayGroundBottom;
        offset = new Vector2(0, check_offset_top);
        onOneWayGroundTop = Physics2D.OverlapBox((Vector2)transform.position + offset, new Vector2(check_x_size, check_y_size), 0, oneWayGroundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 offset = new Vector2(0, -check_offset_down);
        Gizmos.DrawWireCube((Vector2)transform.position + offset, new Vector2(check_x_size, check_y_size));
        offset = new Vector2(0, check_offset_top);
        Gizmos.DrawWireCube((Vector2)transform.position + offset, new Vector2(check_x_size, check_y_size));
    }
    void JumpCheck()
    {
        if (onGround && !onGroundEnter)
        {
            AudioManager.PlayFallToGroundAudio();
            isFalling = false;
            jumpTimes = jumpMaxTimes;
            onGroundEnter = true;
        }
        else if (!onGround && onGroundEnter) 
        {
            jumpTimes = jumpMaxTimes-1;
            onGroundEnter = false;
        }
        jumpHold = Input.GetButton("Jump");
        if (Input.GetButtonDown("Jump") && jumpTimes > 0 && (!onOneWayGroundBottom || Input.GetAxis("Vertical") >= -0.5f))
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
    void oneWayGroundCheck()
    {
        if(onOneWayGroundBottom && Input.GetAxis("Vertical") < -0.5f && Input.GetButton("Jump"))
        {
            onOneWayGroundBottom.usedByEffector = false;
            Physics2D.IgnoreLayerCollision(6, 12, true);
        }
        if (onOneWayGroundTop)
        {
            onOneWayGroundTop.usedByEffector = true;
            Physics2D.IgnoreLayerCollision(6, 12, false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BossDoor"))
        {
            canMove = false;
            canChangeAnim = false;
            ChangeAnimationState(anim_run);
            if(transform.position.x < collision.transform.position.x)
            {
                rb.velocity = Vector2.right * Speed;
            }
            else
            {
                rb.velocity = Vector2.left * Speed;
            }
        }
    }
}
