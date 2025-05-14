using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [Header("Player Colliders")]
    [SerializeField] private Collider2D playerCollider;   // ���� �ݶ��̴�
    [SerializeField] private Collider2D slidingCollider;  // �����̵� �ݶ��̴�
    [SerializeField] private Collider2D obstacleDetecter; // ��ֹ� ������

    private PlayerHealth health;
    private PlayerStatusEffects status;
    private int obstacleCount; // ���� ��ֹ� ��
    private int obstacleComboCount; // �������� ���� �ʰ� ���� ��ֹ� ��
    private int damagedTimes; // ������ ���� Ƚ��



    void Awake()
    {
        health = GetComponent<PlayerHealth>();
        status = GetComponent<PlayerStatusEffects>();

    }
    public void ObstacleClear()
    {
        if (health.isDead) return;
        obstacleCount++;
        AchievementManager.Instance.ProgressRate("clear_50", 1);
        AchievementManager.Instance.ProgressRate("clear_100", 1);
        AchievementManager.Instance.ProgressRate("clear_200", 1);
        Debug.Log("Obstacle Clear: " + obstacleCount);
    }
    public void Combo()
    {
        if (health.isDead) return;

        obstacleComboCount++;
        Debug.Log("Obstacle Combo: " + obstacleComboCount); // �޺� ����, ����Ʈ, UI �� �߰�
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
            health.TakeDamage(10f); // ��ֹ��� ����� �� ������ ó��
            damagedTimes++; // ������ ���� Ƚ�� ����
            obstacleComboCount = 0; // ��ֹ� �޺� �ʱ�ȭ
            return;
        }
        if (!playerHit && !slidingHit)
        {
            // ��ֹ��� ���� �ʾ��� �� �޺� ó��
            Combo();
            ObstacleClear();
        }
    }
}