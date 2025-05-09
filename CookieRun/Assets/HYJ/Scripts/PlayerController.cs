using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;

    public float playermaxhealth = 100f; // 최대 체력
    public float jumpForce = 5f; // 점프력
    public float currenthealth; // 현재 플레이어 체력
    private int maxJumps = 2; // 최대 점프 횟수
    private int jumpCount = 0; // 현재 점프 횟수
    private bool isUndamageable = false; // 무적 상태 여부
    private bool isJumping = false; // 점프 중인지 여부
    private bool isDoubleJumping = false; // 더블 점프 중인지 여부
    private bool isSliding = false; // 슬라이딩 중인지 여부
    private bool isGrounded = false; // 바닥에 닿아 있는지 여부
    private bool isDead = false; // 죽음 여부
    private int damagedTimes; // 데미지 입은 횟수
    private int ObstacleCount; // 넘은 장애물 수
    private int ObstacleComboCount; // 데미지를 입지 않고 넘은 장애물 수
    private float movedistance = 0f; // 이동 거리
    public int uiMoveDistance; // UI에 표시할 이동 거리
    public int uicurrenthealth; // UI에 표시할 현재 체력


    private Animator animator;
    private Rigidbody2D _rigidbody;

    [SerializeField] private float healthdecreaseAmount = 0.1f; // 체력 감소량
    [SerializeField] private float healthdecreaseInterval = 0.1f; // 체력 감소 시간
    [SerializeField] private float speedUpInterval = 10f; // 속도 증가 시간
    [SerializeField] private float speedUpAmount = 1f; // 속도 증가량
    [SerializeField] private float undamageable = 1f; // 무적 시간
    [SerializeField] private Collider2D playerCollider; // 플레이어 콜라이더
    [SerializeField] private Collider2D slidingCollider; // 슬라이딩 콜라이더
    [SerializeField] private Collider2D obstacleDetecter; // 장애물 감지기
    [SerializeField] private Collider2D groundDetector; // 바닥 감지기
    [SerializeField] private LayerMask ground;// 바닥 레이어

    void Start()
    {
        gameManager = GameManager.Instance;

        playerCollider.enabled = true; // 플레이어 콜라이더 활성화
        slidingCollider.enabled = false; // 슬라이딩 콜라이더 비활성화
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        currenthealth = playermaxhealth; // 플레이어 체력 초기화
        StartCoroutine(SpeedUp()); // 시간에 따라 속도 증가
        StartCoroutine(HpDecrease()); // 시간에 따라 체력 감소
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>(); //장애물 찾아오기

    }
    private void FixedUpdate()
    {
        CheckGround(); // 바닥 감지
        Move(); // 이동        
    }
    void Update()
    {
        if (isDead)
        {
            // 죽었을 때의 처리
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
        uiMoveDistance = Mathf.FloorToInt(movedistance); // UI에 표시할 이동 거리
        uicurrenthealth = Mathf.FloorToInt(currenthealth); // UI에 표시할 현재 체력
    }

    void Move()
    {
        // 입력 없이 자동으로 이동
        _rigidbody.velocity = new Vector2(gameManager.speed, _rigidbody.velocity.y);
        movedistance += gameManager.speed * Time.deltaTime;
    }
    void HandleJump()
    {
            if (jumpCount == 0 && isGrounded) // 바닥에 닿아있을 때만 점프 가능
        {
                // 점프
                jumpCount = 1;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
                isJumping = true;
                animator.SetBool("IsJump", true);
            }
            else if (!isGrounded && jumpCount < maxJumps) // 공중에 있을 때만 더블점프 가능
            {
                jumpCount = 2;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
                isDoubleJumping = true;
                animator.SetTrigger("IsDoubleJump");
            }
    }

    void StartSlide()
    {
        isSliding = true;
        animator.SetBool("IsSliding", true);

        playerCollider.enabled = false; // 플레이어 콜라이더 비활성화
        slidingCollider.enabled = true; // 슬라이딩 콜라이더 활성화
    }
    void StopSlide()
    {
        isSliding = false;
        animator.SetBool("IsSliding", false);

        playerCollider.enabled = true; // 플레이어 콜라이더 활성화
        slidingCollider.enabled = false; // 슬라이딩 콜라이더 비활성화
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

        StartCoroutine(Undamageable()); // 무적 상태로 전환
        damagedTimes++; // 데미지 입은 횟수 증가
        ObstacleComboCount = 0; // 장애물 콤보 초기화
        currenthealth -= damage; // 체력 감소
        animator.SetTrigger("IsDamage");
        Debug.Log("Player Damaged: " + currenthealth); // 데미지 사운드, 이펙트, UI 등 추가

        if (currenthealth <= 0)
            Die();
    }
    public void Combo()
    {
        if (isDead) return;

        ObstacleComboCount++;
        Debug.Log("Obstacle Combo: " + ObstacleComboCount); // 콤보 사운드, 이펙트, UI 등 추가
    }
    public void ObstacleClear()
    {
        if (isDead) return;
        ObstacleCount++;
        Debug.Log("Obstacle Clear: " + ObstacleCount); // 장애물 클리어 사운드, 이펙트, UI 등 추가
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (!obstacle)
            return;

        bool playerHit = playerCollider.IsTouching(collision);
        bool slidingHit = slidingCollider.IsTouching(collision);

        if (playerHit || slidingHit)
        {
            TakeDamage(10f); // 장애물에 닿았을 때 데미지 처리
            ObstacleComboCount = 0; // 장애물 콤보 초기화
            return;
        }
        if (!playerHit && !slidingHit)
        {
            // 장애물에 닿지 않았을 때 콤보 처리
            Combo();
            ObstacleClear();
        }
    }

    private IEnumerator SpeedUp()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(speedUpInterval);
            gameManager.speed += speedUpAmount;
            Debug.Log("Speed Up: " + gameManager.speed); // 속도 증가 사운드, 이펙트, UI 등 추가
        }
    }

    private IEnumerator HpDecrease()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(healthdecreaseInterval);
            currenthealth -= healthdecreaseAmount;
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
        gameManager.speed = 0f; // 죽었을 때 속도 초기화
        playerCollider.enabled = false; // 플레이어 콜라이더 비활성화
        slidingCollider.enabled = false; // 슬라이딩 콜라이더 비활성화
        Debug.Log("Player is Dead");
        Destroy(gameObject, 2f); // 2초 후에 플레이어 오브젝트 삭제
        // gameManager.GameOver(); // 게임 오버 처리
        // 게임 오버 UI 활성화
    }
}
