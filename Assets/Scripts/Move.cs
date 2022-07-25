using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Move : MonoBehaviour
{
    Easing easing;

    float maxSpeed = 10f;
    float moveSpeed = 1f;
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
    }

    public void Walk ()
    {
        moveX = maxSpeed * Input.GetAxisRaw("Horizontal");
        moveX = Mathf.Lerp(rb.velocity.x, moveX, moveSpeed);
        rb.velocity = new Vector2(Easing.EaseIn(moveX), rb.velocity.y);
        /*if (movmentSpeed > 0 && !isRight) FlipDirection();
        if (movmentSpeed < 0 && isRight) FlipDirection();*/

        /*Vector2 targetVelocity = new Vector2(moveX, vZero);*/
        /*rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movmentSmoothing);*/
        
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
