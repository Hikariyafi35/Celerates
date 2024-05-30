using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    public Rigidbody2D rb2D = null;
    public float speed = 15f;
    public float damage = 20f;
    public float delaySeconds = 3f;

    private WaitForSeconds cullDelay = null;
    private Vector2 direction;
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
    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.layer == 10){
            IDamageableEnemy playerAttributes = collider.GetComponent<IDamageableEnemy>();
            if(playerAttributes != null){
                playerAttributes.TakeDamage(damage);
                Debug.Log("kena damage");
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