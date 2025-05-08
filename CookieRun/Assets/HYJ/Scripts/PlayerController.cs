using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5f; // �ӵ�
    public float jumpForce = 5f; // ����
    public int maxJumps = 2; // �ִ� ���� Ƚ��
    public int jumpCount = 0; // ���� ���� Ƚ��
    public bool isJumping = false; // ���� ������ ����
    public bool isDoubleJumping = false; // ���� ���� ������ ����
    public bool isSliding = false; // �����̵� ������ ����
    public bool isGrounded = false; // �ٴڿ� ��� �ִ��� ����
    public bool isDead = false; // ���� ����

    private Animator animator; // �ִϸ�����
    private Rigidbody2D _rigidbody;
    private BoxCollider2D groundDetector; // �ٴ� ������

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
            // �׾��� ���� ó��
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
        // �Է� ���� �ڵ����� �̵�
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
    }
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            if (jumpCount == 0 && isGrounded)
            {
                // ����
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
