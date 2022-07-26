using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Move : MonoBehaviour
{
    
    Easing Easing;
    Animator Animator;
    SpriteRenderer SpriteRenderer;
    Transform Transform;

    Rigidbody2D rb;
    BoxCollider2D bc2D;

    bool isFacingRight = true;
    bool isJumping = false;
    bool isGrounded = false;

    float heightTestPlayer;
    float maxSpeed = 10f;
    float moveSpeed = 2f;
    public float jumpForce = 5f;
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
        layerMaskGround = LayerMask.GetMask("Ground");
        Animator.SetBool("isIdling", true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Walk();
        }   
        else if (Input.GetAxisRaw("Jump") != 0 && !isJumping && IsGrounded())
        {
            Jump();
        }
        else if (IsGrounded())
        {
            isGrounded = true;
            isJumping = false;
            Animator.SetBool("isJumping", false);
            Animator.SetBool("isWalking", false);
            Animator.SetBool("isIdling", true);
        }
    }

    public void Walk ()
    {
        Animator.SetBool("isIdling", false);
        Animator.SetBool("isWalking", true);
        moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX, rb.velocity.y);
        if (moveX < 0 && isFacingRight)
        {
            Flip();
        }
        else if (moveX > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    public void Jump ()
    {
        isJumping = true;
        Animator.SetBool("isIdling", false);
        Animator.SetTrigger("isJumping");
        moveY = Input.GetAxisRaw("Jump");
        rb.AddForce(new Vector2(rb.velocity.x, moveY) * jumpForce, ForceMode2D.Impulse);
    }


    void Flip ()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    /*protected virtual void UpdateGravity()
    {
        float g = pConfig.gravity * gravityScale * Time.fixedDeltaTime;
        if (speed.y > 0)
        {
            speed.y += g;
        }
        else
        {
            externalForce.y += g;
        }
    }*/

    //private bool IsGrounded()
    //{
    //    var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.7f);
    //    return groundCheck.collider != null && groundCheck.collider.CompareTag("Ground");
    //}

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(bc2D.bounds.center, Vector2.down, heightTestPlayer, layerMaskGround);
        bool isGrounded = hit.collider != null;
        if (isGrounded)
        {
            Animator.SetBool("isGrounded", true);
        }
        else
        {
            Animator.SetBool("isGrounded", false);
        }
        Debug.DrawRay(bc2D.bounds.center, Vector2.down * heightTestPlayer, isGrounded ? Color.green : Color.red, 0.5f);
        
        return isGrounded;
    }

    void OnDrawGizmosSelected()
    {
    #if UNITY_EDITOR
            Gizmos.color = Color.red;

            //Draw the suspension
            Gizmos.DrawLine(
                Vector3.zero,
                Vector3.up  
            );

            //draw force application point
            Gizmos.DrawWireSphere(Vector3.zero, 0.05f);

            Gizmos.color = Color.white;
    #endif
    }

}
