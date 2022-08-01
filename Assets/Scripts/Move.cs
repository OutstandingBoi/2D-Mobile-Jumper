using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Move : MonoBehaviour
{
    public enum PLAYER_STATE
    {
        IDLE, JUMP, WALK, ATTACK
    }
    public PLAYER_STATE currentState = PLAYER_STATE.IDLE;

    Easing Easing;
    Animator Animator;
    SpriteRenderer SpriteRenderer;
    Transform Transform;

    Rigidbody2D rb;
    BoxCollider2D bc2D;

    bool isFacingRight;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool isDashing = false;

    public const float maxDashTime = 1.0f;
    float dashDistance = 10;
    float dashSpeed = 6;
    float heightTestPlayer;
    float airSpeed = 0.7f;
    float jumpForce = 3.5f;
    float moveX = 0f;
    float moveY = 0f;
    float dashInput = 0f;
    int layerMaskGround;


    
    Vector2 vZero = Vector2.zero;
    Vector2 speed = Vector2.zero;
    Vector2 moveDirection;
    Vector3 pos;

    private void Awake()
    {
        pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Transform = GetComponent<Transform>();
        bc2D = GetComponent<BoxCollider2D>();
        heightTestPlayer = bc2D.bounds.extents.y + 0.05f;
        isFacingRight = Transform.localScale.x > 0;
        layerMaskGround = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        InputCheck();
        //Debug.Log(currentState);
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case PLAYER_STATE.IDLE:
                Animator.Play("Idle");
                break;
            case PLAYER_STATE.JUMP:
                if (!isJumping)
                {
                    Jump(moveY);
                }
                break;
            case PLAYER_STATE.WALK:
                if (!IsGrounded())
                {
                    Walk(moveX * airSpeed);
                }
                else
                {
                    Walk(moveX);
                }
                break;
            case PLAYER_STATE.ATTACK:
                break;
        }
    }

    private void InputCheck()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        dashInput = Input.GetAxisRaw("Dash");
        

        if (moveX != 0)
        {
            currentState = PLAYER_STATE.WALK;
        }
        if (moveY != 0 && currentState != PLAYER_STATE.JUMP && IsGrounded())
        {
            
            currentState = PLAYER_STATE.JUMP;
        }
        if (dashInput != 0 && isJumping && !isDashing)
        {
            StartCoroutine(Dash());
        }
        if (moveX == 0 && moveY == 0 & IsGrounded())
        {
            if (currentState != PLAYER_STATE.IDLE)
            {
                StartCoroutine(IdleCheck());
            }
        }
    }

    public void Walk (float input)
    {
        if (isGrounded)
        {
            Animator.Play("Walk");
        }
        rb.velocity = new Vector2(input, rb.velocity.y);
        if (input < 0 && isFacingRight)
        {
            Flip();
        }
        else if (input > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    public void Jump (float input)
    {
        isJumping = true;
        Animator.Play("Jump");
        rb.AddForce(new Vector2(rb.velocity.x, input * jumpForce), ForceMode2D.Impulse);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        Animator.Play("Dash");

        float time = 0;
        moveDirection = pos * dashDistance;
        while (time < 5)
        {
            pos = Vector2.Lerp(pos, moveDirection, time / 5);
            time += Time.fixedDeltaTime;
            yield return null;
        }
        pos = moveDirection;
    }

    void Flip ()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(bc2D.bounds.center, Vector2.down, heightTestPlayer, layerMaskGround);
        bool groundbool = hit.collider != null;
        isGrounded = groundbool;
        if (isGrounded)
        {
            isJumping = false;
            isDashing = false;
        }
        Debug.DrawRay(bc2D.bounds.center, Vector2.down * heightTestPlayer, groundbool ? Color.green : Color.red, 0.5f);
        
        return groundbool;
    }

    IEnumerator IdleCheck()
    {
        if (rb == null) { yield break; }

        while (rb.velocity == Vector2.zero)
        {
            currentState = PLAYER_STATE.IDLE;
            yield break;
        }

        yield break;
    }

}
