using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float heatlhPool = 10f;

    public float speed = 5f;
    public float jumpForce = 6f;
    public float groundedLeeway = 0.1f;
    

    public Rigidbody2D rb2d = null;
    private Animator animator = null;
    public float currentHealth = 1f;

    public Rigidbody2D Rb2D{
        get { return rb2d; }
        protected set { rb2d = value; }
    }
    public Animator Animator{
        get {return animator;}
        protected set { animator = value; }
    }
    public float CurrentHealth{
        get { return currentHealth; }
        protected set { currentHealth = value; }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if(GetComponent<Rigidbody2D>()){
            rb2d = GetComponent<Rigidbody2D>();
        }
        if(GetComponent<Animator>()){
            animator = GetComponent<Animator>();
        }
        currentHealth = heatlhPool;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected bool GroundCheck(){
        return Physics2D.Raycast(transform.position, -Vector2.up, groundedLeeway);
    }
    protected virtual void Die(){
        gameObject.SetActive(false);
        Destroy(gameObject);
        Debug.Log("Enemy died");
    }
}
