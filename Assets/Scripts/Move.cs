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

    float heightTestPlayer;
    float airSpeed = 0.7f;
    float jumpForce = 3.5f;
    float moveX = 0f;
    float moveY = 0f;
    int layerMaskGround;


    Vector2 vZero = Vector2.zero;
    Vector2 speed = Vector2.zero;
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

    private void FixedUpdate()
    {
        InputCheck();
        Debug.Log(currentState);
    }

    private void InputCheck()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0)
        {
            if (!IsGrounded())
            {
                Walk(moveX * airSpeed);
            }
            else
            {
                Walk(moveX);
            }
        }
        if (moveY != 0 && currentState != PLAYER_STATE.JUMP && IsGrounded())
        {
            Jump(moveY);
        }
        if (moveX == 0 && moveY == 0)
        {
            if (currentState != PLAYER_STATE.IDLE)
            {
                StartCoroutine(IdleCheck());
            }
        }
    }

    public void Walk (float input)
    {
        currentState = PLAYER_STATE.WALK;
        Animator.Play("Walk");
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
        currentState = PLAYER_STATE.JUMP;
        Animator.Play("Jump");
        rb.AddForce(new Vector2(rb.velocity.x, input * jumpForce), ForceMode2D.Impulse);
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
        Debug.DrawRay(bc2D.bounds.center, Vector2.down * heightTestPlayer, groundbool ? Color.green : Color.red, 0.5f);
        
        return groundbool;
    }

    IEnumerator IdleCheck()
    {
        if (rb == null) { yield break; }

        while (rb.velocity == Vector2.zero)
        {
            currentState = PLAYER_STATE.IDLE;
            Animator.Play("Idle");
            yield break;
        }

        yield break;
    }

}
