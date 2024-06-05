using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform player;
    public Transform patrolPointA;
    public Transform patrolPointB;
    public float speed = 2f;
    public float attackRange = 5f;
    public float attackCooldown = 1f;
    public int attackDamage = 10;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private Transform currentPatrolTarget;
    private float lastAttackTime;
    private bool facingRight = true;
    private Animator animator;
    private bool isDead = false;
    private float timeUntilRangedReadied = 0f; // Timer untuk cooldown serangan jarak jauh
    private Rigidbody2D rb;
    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        currentPatrolTarget = patrolPointA;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isDead = animator.GetBool("isDead");
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
        //animator.SetBool("isRunning", !isDead && !animator.GetBool("isAttacking"));

        // Decrease cooldown timer
        if (timeUntilRangedReadied > 0)
        {
            timeUntilRangedReadied -= Time.deltaTime;
        }
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

        if (timeUntilRangedReadied <= 0)
        {
            // Set attacking animation
            animator.SetBool("isAttacking", true);
            audioManager.playSfx(audioManager.idleEnemyRangeAttack);
            ShootProjectile();
            lastAttackTime = Time.time;
            timeUntilRangedReadied = attackCooldown; // Reset cooldown timer
        }
    }

    void ShootProjectile()
    {
        // Adjust the rotation of the projectile based on the enemy's facing direction
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        Quaternion projectileRotation = Quaternion.Euler(0, 0, facingRight ? 0 : 180);

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, projectileRotation);
        projectile.GetComponent<ProjectileEnemy>().Initialize(direction);
    }

    void FlipSprite(bool faceRight)
    {
        facingRight = faceRight;
        Vector3 theScale = transform.localScale;
        theScale.x = facingRight ? Mathf.Abs(theScale.x) : -Mathf.Abs(theScale.x);
        transform.localScale = theScale;

        // Rotate firePoint to face the correct direction
        Vector3 firePointRotation = firePoint.localEulerAngles;
        firePointRotation.y = facingRight ? 0 : 180;
        firePoint.localEulerAngles = firePointRotation;
    }

    // Method untuk menerima damage
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        // Set hurt animation
        //animator.SetTrigger("isHurt");

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
        Debug.Log("range enemy tewas");
        
        // Logika kematian enemy di sini (misalnya, disable collider, dll.)

    }

    // Menggambar gizmo untuk radius serangan
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}