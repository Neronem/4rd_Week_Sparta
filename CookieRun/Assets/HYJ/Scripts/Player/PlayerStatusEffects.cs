using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffects : MonoBehaviour
{
    // Start is called before the first frame update
    public float unDamageable = 1f;
    public bool isUndamageable;

    public float speedUpInterval = 10f;
    public float speedUpAmount = 1f;

    [SerializeField] private float healthdecreaseAmount = 0.1f; // 체력 감소량
    [SerializeField] private float healthdecreaseInterval = 0.1f; // 체력 감소 시간

    private PlayerMovement movement;
    private PlayerHealth health;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
    }



    public IEnumerator Undamageable()
    {
        isUndamageable = true;
        yield return new WaitForSeconds(unDamageable);
        isUndamageable = false;
    }

    public IEnumerator SpeedUpRoutine()
    {
        while (health.IsDead)
        {
            yield return new WaitForSeconds(speedUpInterval);
            movement.speed += speedUpAmount;
        }
    }
    public IEnumerator HpDecrease()
    {
        while (!health.IsDead)
        {
            yield return new WaitForSeconds(healthdecreaseInterval);
            health.currentHealth -= healthdecreaseAmount;
            if (health.currentHealth <= 0)
                health.Die();
        }
    }

}

