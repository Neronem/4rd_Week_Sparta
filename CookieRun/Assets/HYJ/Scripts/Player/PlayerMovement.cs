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
    private bool isJumping = false; // ���� ������ ����
    private bool isDoubleJumping = false; // ���� ���� ������ ����

    public AudioClip jumpAudio;
    public AudioClip doubleJumpAudio;
    public AudioClip slideAudio;
    
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
        if (jumpCount == 0 && isGrounded && Input.GetKeyDown(KeyCode.Space)) // �ٴڿ� ������� ���� ���� ����
        {
            // ����
            jumpCount = 1;
            if(jumpAudio != null) 
                SoundManager.PlayClip(jumpAudio);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
            isJumping = true;
            animator.SetBool("IsJump", true);
        }
        else if (!isGrounded && jumpCount < maxJumps && Input.GetKeyDown(KeyCode.Space)) // ���߿� ���� ���� �������� ����
        {
            jumpCount = 2;
            if(doubleJumpAudio != null) 
                SoundManager.PlayClip(doubleJumpAudio);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
            isDoubleJumping = true;
            animator.SetTrigger("IsDoubleJump");
        }
    }

    public void HandleSlide()
    {
        // Shift ������ ���ȸ� �����̵�
        if (isGrounded && Input.GetKey(KeyCode.LeftShift) && !isSliding)
        {
            if(slideAudio != null) 
                SoundManager.PlayClip(slideAudio);
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
