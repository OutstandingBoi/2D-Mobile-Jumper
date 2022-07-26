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

    bool isFacingRight = true;
    float maxSpeed = 10f;
    float moveSpeed = 2f;
    Rigidbody2D rb;
    float moveX = 0f;
    float moveY = 0f;
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
        Animator.SetBool("isIdling", true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.anyKey)
        {
            Walk();
        }
        else
        {
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
        else { }

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

    private bool IsGrounded()
    {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.7f);
        return groundCheck.collider != null && groundCheck.collider.CompareTag("Ground");
    }


}
