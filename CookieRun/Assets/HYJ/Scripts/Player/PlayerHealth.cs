using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    private CameraShake cameraShake;
    public float shakeDuration = 1f;
    public float shakeMagnitude = 1f;
    private Animator animator;
    private PlayerStatusEffects statusEffects;
    public bool isDead = false;

    public AudioClip damagedAudio; 


    void Awake()
    {
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (camera != null)
        {
            cameraShake = camera.GetComponent<CameraShake>();
        }
        currentHealth = maxHealth;
        statusEffects = GetComponent<PlayerStatusEffects>();
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(float amount)
    {
        if (isDead || statusEffects.isUndamageable) return;

        StartCoroutine(statusEffects.Undamageable());
        if(damagedAudio != null)
            SoundManager.PlayClip(damagedAudio);
        currentHealth -= amount;
        animator.SetTrigger("IsDamage");
        if (cameraShake != null && !cameraShake.isShaking)
        {
            StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
        }
        if (currentHealth <= 0f)
            Die();
    }

    public void Heal(float amount)
    {
        if (isDead) return;
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("IsDead");
        Destroy(gameObject, 2f); // 2�� �Ŀ� ������Ʈ ����
    }
}
