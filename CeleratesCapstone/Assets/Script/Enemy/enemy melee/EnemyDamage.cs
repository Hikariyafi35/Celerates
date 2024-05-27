using UnityEngine;

public class EnemyDamage: MonoBehaviour
{
    [SerializeField] protected float damage;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player")collision.GetComponentInChildren<HealthSystem>().TakeDamage(damage);
    }
}