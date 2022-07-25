using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {

    /*public PlayerController playerController;*/

    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        /*ReadPlayerStateAndAnimate();*/
    }

    // Update is called once per frame
    void Update()
    {
        /*ReadPlayerStateAndAnimate();*/
    }

    /// <summary>
    /// Description:
    /// Reads the player's state and then sets and unsets booleans in the animator accordingly
    /// Input:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    /*void ReadPlayerStateAndAnimate()
    {
        if (m_Animator == null)
        {
            return;
        }
        if (playerController.state == PlayerController.PlayerState.Idle)
        {
            m_Animator.SetBool("isIdle", true);
        }
        else
        {
            m_Animator.SetBool("isIdle", false);
        }

        if (playerController.state == PlayerController.PlayerState.Jump)
        {
            m_Animator.SetBool("isJumping", true);
        }
        else
        {
            m_Animator.SetBool("isJumping", false);
        }

        if (playerController.state == PlayerController.PlayerState.Fall)
        {
            m_Animator.SetBool("isFalling", true);
        }
        else
        {
            m_Animator.SetBool("isFalling", false);
        }

        if (playerController.state == PlayerController.PlayerState.Walk)
        {
            m_Animator.SetBool("isWalking", true);
        }
        else
        {
            m_Animator.SetBool("isWalking", false);
        }

        if (playerController.state == PlayerController.PlayerState.Dead)
        {
            m_Animator.SetBool("isDead", true);
        }
        else
        {
            m_Animator.SetBool("isDead", false);
        }
    }*/
}
