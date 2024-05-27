using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeeleAI : MonoBehaviour
{
    public Transform player;
    public Transform patrolPointA;
    public Transform patrolPointB;
    public float speed = 2f;
    public float attackRange = 5f;
    public float attackCooldown = 1f;
    public int damage = 10;

    private Transform currentPatrolTarget;
    private float lastAttackTime;
    private bool facingRight = true;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentPatrolTarget = patrolPointA;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else
        {
            Patrol();
        }

        // Update animator parameters
        animator.SetBool("isRunning", !isDead && !animator.GetBool("isAttacking"));
    }

    void Patrol()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentPatrolTarget.position, step);

        if (Vector3.Distance(transform.position, currentPatrolTarget.position) < 0.1f)
        {
            // Change patrol target and flip sprite direction
            if (currentPatrolTarget == patrolPointA)
            {
                currentPatrolTarget = patrolPointB;
                FlipSprite(true);
            }
            else
            {
                currentPatrolTarget = patrolPointA;
                FlipSprite(false);
            }
        }
        else
        {
            // Ensure the sprite faces the direction of movement
            if ((currentPatrolTarget.position.x > transform.position.x && !facingRight) ||
                (currentPatrolTarget.position.x < transform.position.x && facingRight))
            {
                FlipSprite(!facingRight);
            }
        }
    }

    void FaceTarget(Vector3 target)
    {
        // Ensure the sprite faces the direction of the target
        if ((target.x > transform.position.x && !facingRight) ||
            (target.x < transform.position.x && facingRight))
        {
            FlipSprite(!facingRight);
        }
    }

    void AttackPlayer()
    {
        FaceTarget(player.position);

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Set attacking animation
            animator.SetBool("isAttacking", true);
            DealDamage();
            lastAttackTime = Time.time;
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    void DealDamage()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            // Mengurangi health player di sini
            player.GetComponent<HealthSystem>().TakeDamage(damage);
        }
    }

    void FlipSprite(bool faceRight)
    {
        facingRight = faceRight;
        Vector3 theScale = transform.localScale;
        theScale.x = facingRight ? Mathf.Abs(theScale.x) : -Mathf.Abs(theScale.x);
        transform.localScale = theScale;
    }

    // Method untuk menerima damage
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        // Set hurt animation
        animator.SetTrigger("isHurt");

        // Implementasi logika pengurangan health di sini
        // Jika health <= 0, set isDead dan mainkan animasi die
        // Misalnya:
        // currentHealth -= damage;
        // if (currentHealth <= 0)
        // {
        //     Die();
        // }
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        // Logika kematian enemy di sini (misalnya, disable collider, dll.)
    }

    // Menggambar gizmo untuk radius serangan
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}