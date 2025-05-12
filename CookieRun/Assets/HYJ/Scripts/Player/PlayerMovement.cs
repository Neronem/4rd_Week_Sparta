using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 20f;
    public int maxJumps = 2;
   
    public Collider2D playerCollider;
    public Collider2D slideCollider;

    public Collider2D groundDetector;
    public LayerMask groundLayer;

    private Rigidbody2D _rigidbody;
    private Animator animator;
    private int jumpCount;
    private bool isGrounded;
    private bool isSliding;
    private bool isJumping = false; // 점프 중인지 여부
    private bool isDoubleJumping = false; // 더블 점프 중인지 여부


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        playerCollider.enabled = true;
        slideCollider.enabled = false;
    }

    public void CheckGround()
    {
        isGrounded = groundDetector.IsTouchingLayers(groundLayer);
        if (isGrounded)
        {
            isJumping = false;
            isDoubleJumping = false;
            animator.SetBool("IsJump", false);
            animator.SetBool("IsGround", true);
            jumpCount = 0;
        }
        else
        {
            animator.SetBool("IsJump", true);
            animator.SetBool("IsGround", false);
        }
    }

    public void HandleJump()
    {
        if (jumpCount == 0 && isGrounded && Input.GetKeyDown(KeyCode.Space)) // 바닥에 닿아있을 때만 점프 가능
        {
            // 점프
            jumpCount = 1;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
            isJumping = true;
            animator.SetBool("IsJump", true);
        }
        else if (!isGrounded && jumpCount < maxJumps && Input.GetKeyDown(KeyCode.Space)) // 공중에 있을 때만 더블점프 가능
        {
            jumpCount = 2;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
            isDoubleJumping = true;
            animator.SetTrigger("IsDoubleJump");
        }
    }

    public void HandleSlide()
    {
        // Shift 누르는 동안만 슬라이드
        if (isGrounded && Input.GetKey(KeyCode.LeftShift) && !isSliding)
        {
            isSliding = true;
            animator.SetBool("IsSliding", true);
            playerCollider.enabled = false;
            slideCollider.enabled = true;
        }
        else if (isSliding && Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSliding = false;
            animator.SetBool("IsSliding", false);
            playerCollider.enabled = true;
            slideCollider.enabled = false;
        }
    }

}
