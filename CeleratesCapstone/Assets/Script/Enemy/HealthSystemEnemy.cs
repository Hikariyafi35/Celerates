using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthSystemEnemy : MonoBehaviour,IDamageable

{
    public float maxHealth = 100f;
    private float currentHealth;

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
        // Add death animation or effect here
        animator.SetBool("isDead", true);
        StartCoroutine(DestroyTimer());
        rb.gravityScale = 1;
    }
    
    IEnumerator DestroyTimer(){
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
