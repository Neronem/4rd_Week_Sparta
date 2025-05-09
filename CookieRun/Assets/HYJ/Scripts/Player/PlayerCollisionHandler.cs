using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [Header("Player Colliders")]
    [SerializeField] private Collider2D playerCollider;   // 몸통 콜라이더
    [SerializeField] private Collider2D slidingCollider;  // 슬라이드 콜라이더
    [SerializeField] private Collider2D obstacleDetecter; // 장애물 감지기

    private PlayerHealth health;
    private PlayerStatusEffects status;
    private int obstacleCount; // 넘은 장애물 수
    private int obstacleComboCount; // 데미지를 입지 않고 넘은 장애물 수
    private int damagedTimes; // 데미지 입은 횟수



    void Awake()
    {
        health = GetComponent<PlayerHealth>();
        status = GetComponent<PlayerStatusEffects>();

    }
    public void ObstacleClear()
    {
        if (health.isDead) return;
        obstacleCount++;
        AchievementManager.Instance.ProgressRate("clear_10", 1);
        Debug.Log("Obstacle Clear: " + obstacleCount); // 장애물 클리어 사운드, 이펙트, UI 등 추가
    }
    public void Combo()
    {
        if (health.isDead) return;

        obstacleComboCount++;
        Debug.Log("Obstacle Combo: " + obstacleComboCount); // 콤보 사운드, 이펙트, UI 등 추가
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
            health.TakeDamage(10f); // 장애물에 닿았을 때 데미지 처리
            damagedTimes++; // 데미지 입은 횟수 증가
            obstacleComboCount = 0; // 장애물 콤보 초기화
            return;
        }
        if (!playerHit && !slidingHit)
        {
            // 장애물에 닿지 않았을 때 콤보 처리
            Combo();
            ObstacleClear();
        }
    }
}