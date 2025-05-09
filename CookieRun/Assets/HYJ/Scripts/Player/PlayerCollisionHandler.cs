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
    private int ObstacleCount; // ���� ��ֹ� ��
    private int ObstacleComboCount; // �������� ���� �ʰ� ���� ��ֹ� ��
    private int damagedTimes; // ������ ���� Ƚ��



    void Awake()
    {
        health = GetComponent<PlayerHealth>();
        status = GetComponent<PlayerStatusEffects>();

    }
    public void ObstacleClear()
    {
        if (health.IsDead) return;
        ObstacleCount++;
        Debug.Log("Obstacle Clear: " + ObstacleCount); // ��ֹ� Ŭ���� ����, ����Ʈ, UI �� �߰�
    }
    public void Combo()
    {
        if (health.IsDead) return;

        ObstacleComboCount++;
        Debug.Log("Obstacle Combo: " + ObstacleComboCount); // �޺� ����, ����Ʈ, UI �� �߰�
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
            ObstacleComboCount = 0; // ��ֹ� �޺� �ʱ�ȭ
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