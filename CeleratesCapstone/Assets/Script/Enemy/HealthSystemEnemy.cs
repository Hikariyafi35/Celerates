using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthSystemEnemy : MonoBehaviour,IDamageable

{
    public float maxHealth = 100f;
    private float currentHealth;
    private Animator animator;
    private Rigidbody2D rb;
    public WinConditionManager winConditionManager; // Referensi ke WinConditionManager

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (winConditionManager == null)
        {
            Debug.LogError("WinConditionManager is not assigned in HealthSystemEnemy");
        }
    }

    public void TakeDamageEnemy(float damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage!");
        animator.SetTrigger("isHurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        animator.SetBool("isDead", true);
        StartCoroutine(DestroyTimer());
        rb.gravityScale = 1;

        // Panggil metode EnemyDefeated() dari WinConditionManager
        if (winConditionManager != null)
        {
            winConditionManager.EnemyDefeated();
        }
        else
        {
            Debug.LogError("WinConditionManager is not assigned in HealthSystemEnemy");
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}