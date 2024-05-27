using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeeleAttack : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackRange = 2f;
    public LayerMask playerLayer;
    public float attackCooldown = 1f; // Delay antara serangan
    public Animator animator; // Reference to the Animator component

    private Transform player;
    private bool canAttack = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (canAttack && Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Lakukan serangan hanya jika pemain dalam jangkauan dan cooldown berakhir
        if (canAttack)
        {
            // Mulai animasi serangan
            animator.SetTrigger("Attack");

            // Lakukan serangan ke pemain
            HealthSystem playerHealth = player.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

            // Mulai cooldown antara serangan
            canAttack = false;
            Invoke("ResetAttack", attackCooldown);
        }
    }

    void ResetAttack()
    {
        // Setelah cooldown berakhir, musuh dapat menyerang lagi
        canAttack = true;
    }
}