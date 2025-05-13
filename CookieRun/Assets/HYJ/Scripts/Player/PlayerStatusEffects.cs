using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffects : MonoBehaviour
{
    public float unDamageable = 1f;
    public bool isUndamageable;

    public bool isSuper = false; // 슈퍼모드 : 아이템먹고 일정시간 무적, 장애물파괴가능상태
    public float superDuration = 5f;

    public float speedUpInterval = 2f;
    public float speedUpAmount = 3f;

    [SerializeField] private float healthdecreaseAmount = 0.1f; // 체력 감소량
    [SerializeField] private float healthdecreaseInterval = 0.1f; // 체력 감소 시간

    private PlayerMovement movement;
    private PlayerHealth health;
    public GameManager gameManager;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;

    }

    public IEnumerator Undamageable()
    {
        isUndamageable = true;
        yield return new WaitForSeconds(unDamageable);
        isUndamageable = false;
    }

    public IEnumerator SpeedUpRoutine()
    {
        while (!health.isDead)
        {
            yield return new WaitForSeconds(speedUpInterval);
            gameManager.speed += speedUpAmount;
            Debug.Log("Speed Up: " + gameManager.speed);
        }
    }
    public IEnumerator HpDecrease()
    {
        while (!health.isDead)
        {
            yield return new WaitForSeconds(healthdecreaseInterval);
            health.currentHealth -= healthdecreaseAmount;
            if (health.currentHealth <= 0)
                health.Die();
        }
    }
    public IEnumerator SuperRoutine()
    {
        isSuper = true;

        // 여기서 파티클, 이펙트, 애니메이션 등 켜도 됨
        // ex: effect.SetActive(true);

        yield return new WaitForSeconds(superDuration);

        isSuper = false;

        // 효과 끄기
        // ex: effect.SetActive(false);
    }

}

