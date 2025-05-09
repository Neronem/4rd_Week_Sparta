using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;

    public float playermaxhealth = 100f; // �ִ� ü��
    public float jumpForce = 5f; // ������
    private float currenthealth; // ���� �÷��̾� ü��
    private int maxJumps = 2; // �ִ� ���� Ƚ��
    private int jumpCount = 0; // ���� ���� Ƚ��
    private bool isUndamageable = false; // ���� ���� ����
    private bool isJumping = false; // ���� ������ ����
    private bool isDoubleJumping = false; // ���� ���� ������ ����
    private bool isSliding = false; // �����̵� ������ ����
    private bool isGrounded = false; // �ٴڿ� ��� �ִ��� ����
    private bool isDead = false; // ���� ����
    private int damagedTimes; // ������ ���� Ƚ��
    private int ObstacleCount; // ���� ��ֹ� ��
    private int ObstacleComboCount; // �������� ���� �ʰ� ���� ��ֹ� ��

    private Animator animator;
    private Rigidbody2D _rigidbody;

    [SerializeField] private float healthdecreaseAmount = 0.1f; // ü�� ���ҷ�
    [SerializeField] private float healthdecreaseInterval = 0.1f; // ü�� ���� �ð�
    [SerializeField] private float speedUpInterval = 10f; // �ӵ� ���� �ð�
    [SerializeField] private float speedUpAmount = 1f; // �ӵ� ������
    [SerializeField] private float undamageable = 1f; // ���� �ð�
    [SerializeField] private Collider2D playerCollider; // �÷��̾� �ݶ��̴�
    [SerializeField] private Collider2D slidingCollider; // �����̵� �ݶ��̴�
    [SerializeField] private Collider2D groundDetector; // �ٴ� ������
    [SerializeField] private LayerMask ground;// �ٴ� ���̾�

    void Start()
    {
        gameManager = GameManager.Instance;

        playerCollider.enabled = true; // �÷��̾� �ݶ��̴� Ȱ��ȭ
        slidingCollider.enabled = false; // �����̵� �ݶ��̴� ��Ȱ��ȭ
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        currenthealth = playermaxhealth; // �÷��̾� ü�� �ʱ�ȭ
        StartCoroutine(SpeedUp()); // �ð��� ���� �ӵ� ����
        StartCoroutine(HpDecrease()); // �ð��� ���� ü�� ����
    }
    private void FixedUpdate()
    {
        CheckGround(); // �ٴ� ����
        Move(); // �̵�
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
        if (currenthealth <= 0)
        {
            Die();
        }
    }

    void Move()
    {
        // �Է� ���� �ڵ����� �̵�
        _rigidbody.velocity = new Vector2(gameManager.speed, _rigidbody.velocity.y);
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
            isJumping = false;
            isDoubleJumping = false;
            animator.SetBool("IsJump", false);
            animator.SetBool("IsGround", true);
        }
        else
        {
            animator.SetBool("IsJump", true);
            animator.SetBool("IsGround", false);
        }
    }
    public void TakeDamage(float damage)
    {
        if (isDead || isUndamageable) return;

        StartCoroutine(Undamageable()); // ���� ���·� ��ȯ
        damagedTimes++; // ������ ���� Ƚ�� ����
        ObstacleComboCount = 0; // ��ֹ� �޺� �ʱ�ȭ
        currenthealth -= damage; // ü�� ����
        animator.SetTrigger("IsDamage");

        if (currenthealth <= 0)
            Die();
    }
    public void Combo()
    {
        if (isDead) return;

        ObstacleComboCount++;
        Debug.Log("Obstacle Combo: " + ObstacleComboCount); // �޺� ����, ����Ʈ, UI �� �߰�
    }

    private IEnumerator SpeedUp()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(speedUpInterval);
            gameManager.speed += speedUpAmount;
            Debug.Log("Speed Up: " + gameManager.speed); // �ӵ� ���� ����, ����Ʈ, UI �� �߰�
        }
    }

    private IEnumerator HpDecrease()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(healthdecreaseInterval);
            currenthealth -= healthdecreaseAmount;
            Debug.Log("Health Decrease: " + currenthealth); // ü�� ���� ����, ����Ʈ, UI �� �߰�
            if (currenthealth <= 0)
                Die();
        }
    }

    private IEnumerator Undamageable()
    {
        isUndamageable = true;
        yield return new WaitForSeconds(undamageable);
        isUndamageable = false;
    }
    public void Heal(float amount)
    {
        currenthealth += amount;
        if (currenthealth > playermaxhealth)
        {
            currenthealth = playermaxhealth;
        }
    }
    private void Die()
    {
        isDead = true;
        animator.SetTrigger("IsDead");
        gameManager.speed = 0f; // �׾��� �� �ӵ� �ʱ�ȭ
        playerCollider.enabled = false; // �÷��̾� �ݶ��̴� ��Ȱ��ȭ
        slidingCollider.enabled = false; // �����̵� �ݶ��̴� ��Ȱ��ȭ
        Debug.Log("Player is Dead");
        Destroy(gameObject, 2f); // 2�� �Ŀ� �÷��̾� ������Ʈ ����
        // gameManager.GameOver(); // ���� ���� ó��
        // ���� ���� UI Ȱ��ȭ
    }
}
