using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleAttackPlayer : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 20;
    public float attackRate = 2f; // Frekuensi serangan dalam serangan per detik

    private float nextAttackTime = 0f;
    private Animator animator;
    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1")) // Default left mouse button or Ctrl key
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // Play attack animation
        if (animator != null)
        {
            animator.SetTrigger("Attack");
            audioManager.playSfx(audioManager.attack);
        }
    }

    // This function will be called by the Animation Event
    void DealDamage()
    {
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            HealthSystemEnemy healthSystemenemy = enemy.GetComponent<HealthSystemEnemy>();
            if (healthSystemenemy != null)
            {
                healthSystemenemy.TakeDamageEnemy(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
