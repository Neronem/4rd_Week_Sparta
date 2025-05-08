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

    [SerializeField] private Collider2D playerCollider; // �÷��̾� �ݶ��̴�
    [SerializeField] private Collider2D slidingCollider; // �����̵� �ݶ��̴�
    [SerializeField] private Collider2D groundDetector; // �ٴ� ������
    [SerializeField] private LayerMask ground;// �ٴ� ���̾�

    void Start()
    {
        playerCollider.enabled = true;
        slidingCollider.enabled = false;
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        CheckGround();
        Move();
    }
    void Update()
    {
        if (isDead)
        {
            // �׾��� ���� ó��
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
            {
                HandleJump();
            }
            if (Input.GetKey(KeyCode.LeftShift) && isGrounded && !isSliding)
            {
                StartSlide();
                Debug.Log("Sliding");
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) && isSliding || !isGrounded)
            {
                StopSlide();
            }
        }
    }

    void Move()
    {
        // �Է� ���� �ڵ����� �̵�
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
    }
    void HandleJump()
    {
            if (jumpCount == 0 && isGrounded) // �ٴڿ� ������� ���� ���� ����
        {
                // ����
                jumpCount = 1;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
                isJumping = true;
                animator.SetBool("IsJump", true);
                Debug.Log("First Jump");
            }
            else if (!isGrounded && jumpCount < maxJumps) // ���߿� ���� ���� �������� ����
            {
                jumpCount = 2;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
                isDoubleJumping = true;
                animator.SetTrigger("IsDoubleJump");
                Debug.Log("Double Jump");
            }
    }

    void StartSlide()
    {
        isSliding = true;
        animator.SetBool("IsSliding", true);

        playerCollider.enabled = false; // �÷��̾� �ݶ��̴� ��Ȱ��ȭ
        slidingCollider.enabled = true; // �����̵� �ݶ��̴� Ȱ��ȭ
    }
    void StopSlide()
    {
        isSliding = false;
        animator.SetBool("IsSliding", false);

        playerCollider.enabled = true; // �÷��̾� �ݶ��̴� Ȱ��ȭ
        slidingCollider.enabled = false; // �����̵� �ݶ��̴� ��Ȱ��ȭ
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
