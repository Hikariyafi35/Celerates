using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb2D = null;
    public float speed = 15f;
    public float damage = 20f;
    public float delaySeconds = 3f;

    private WaitForSeconds cullDelay = null;
    void Start()
    {
        cullDelay = new WaitForSeconds(delaySeconds);
        StartCoroutine(DelayedCull());
        rb2D.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.layer == 9){
            IDamageable enemyAttributes = collider.GetComponent<IDamageable>();
            if(enemyAttributes != null){
                enemyAttributes.TakeDamageEnemy(damage);
            }
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    private IEnumerator DelayedCull(){
        yield return cullDelay;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
