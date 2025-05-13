using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    SkinManager skinManager;
    private PlayerMovement movement;
    private PlayerHealth health;
    private PlayerStatusEffects statusEffects;
    private PlayerCollisionHandler collisionHandler;

    private Animator animator;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
        statusEffects = GetComponent<PlayerStatusEffects>();
        collisionHandler = GetComponent<PlayerCollisionHandler>();
    }
    void Start()
    {
        skinManager = SkinManager.Instance;
        gameManager = GameManager.Instance;

        if (skinManager != null)
            skinManager.OnPlayerSpawn(gameObject);
        else
            Debug.LogWarning("PlayerController: SkinManager.Instance가 null입니다. 스킨이 적용되지 않습니다.");


        StartCoroutine(statusEffects.SpeedUpRoutine());
        StartCoroutine(statusEffects.HpDecrease());
    }
    private void FixedUpdate()
    {
        movement.CheckGround();
        _rigidbody.velocity = new Vector2(gameManager.speed, _rigidbody.velocity.y);
        
    }
    void Update()
    {
        movement.HandleJump();
        movement.HandleSlide();
    
        if (health.currentHealth <= 0)
        {
            gameManager.speed = 0f;
            movement.playerCollider.enabled = false;
            movement.slideCollider.enabled = false;
            movement.groundDetector.enabled = false;
            _rigidbody.velocity = Vector2.zero;
            health.Die();
            GameManager.Instance.GameOver();
        }

        if (transform.position.y < -6)
        {
            gameManager.speed = 0f;
            movement.playerCollider.enabled = false;
            movement.slideCollider.enabled = false;
            movement.groundDetector.enabled = false;
            health.Die();
            GameManager.Instance.GameOver();
        }
    }

}
