using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;
    private Animator animator;
    private PlayerStatusEffects statusEffects;
    public bool IsDead = false;


    void Awake()
    {
        currentHealth = maxHealth;
        statusEffects = GetComponent<PlayerStatusEffects>();
        animator = GetComponentInChildren<Animator>();

    }

    public void TakeDamage(float amount)
    {
        if (IsDead || statusEffects.isUndamageable) return;

        StartCoroutine(statusEffects.Undamageable());
        currentHealth -= amount;
        animator.SetTrigger("IsDamage");
        if (currentHealth <= 0f)
            Die();
    }

    public void Heal(float amount)
    {
        if (IsDead) return;
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Die()
    {
        IsDead = true;
        animator.SetTrigger("IsDead");
    }
}
