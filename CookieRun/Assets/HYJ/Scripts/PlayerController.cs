using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5f; // 속도
    public float jumpForce = 5f; // 점프
    public int maxJumps = 2; // 최대 점프 횟수
    public int jumpCount = 0; // 현재 점프 횟수
    public bool isJumping = false; // 점프 중인지 여부
    public bool isDoubleJumping = false; // 더블 점프 중인지 여부
    public bool isSliding = false; // 슬라이딩 중인지 여부
    public bool isGrounded = false; // 바닥에 닿아 있는지 여부
    public bool isDead = false; // 죽음 여부

    private Animator animator; // 애니메이터
    private Rigidbody2D _rigidbody;
    private BoxCollider2D groundDetector; // 바닥 감지기

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        groundDetector = GetComponentInChildren<BoxCollider2D>();
    }

    void Update()
    {
        if (isDead)
        {
            // 죽었을 때의 처리
        }
        else
        {
            CheckGround();
            Move();
            HandleJump();
            Slide();
        }
    }

    void Move()
    {
        // 입력 없이 자동으로 이동
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
    }
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            if (jumpCount == 0 && isGrounded)
            {
                // 점프
                jumpCount = 1;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
                isJumping = true;
                animator.SetBool("IsJump", true);
                Debug.Log("First Jump");
            }
            else if (!isGrounded && jumpCount < maxJumps)
            {
                jumpCount++;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
                isDoubleJumping = true;
                animator.SetTrigger("IsDoubleJump");
                Debug.Log("Double Jump");
            }
        }
    }
    void Slide()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSliding && isGrounded)
        {
            isSliding = true;
            animator.SetBool("IsSliding", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isSliding)
        {
            isSliding = false;
            animator.SetBool("IsSliding", false);
        }
    }
    void CheckGround()
    {
        isGrounded = groundDetector.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (isGrounded)
        {
            jumpCount = 0;
            animator.SetBool("IsJump", false);
            animator.SetBool("IsGround", true);
        }
        else
        {
            animator.SetBool("IsJump", true);
            animator.SetBool("IsGround", false);
        }
    }
    public void TakeDamage()
    {
        animator.SetTrigger("IsDamage");
    }
}
