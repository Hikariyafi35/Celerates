using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character , IDamageAble
{   
    [Header("Input")]
    public KeyCode meeleAttackKey = KeyCode.Mouse0;
    public KeyCode rangeAttackKey = KeyCode.Mouse1;
    public KeyCode jumpKey = KeyCode.Space;
    public string xMoveAxis = "Horizontal";

    [Header("Combat")]
    public Transform meeleAttackOrigin = null;
    public Transform rangeAttackOrigin = null;
    public GameObject Projectile = null;
    public float meeleAttackRadius = 0.6f;
    public float meeledamage = 2f;
    public float meeleattackDelay = 1.1f;
    public float rangeAttackDelay = 0.3f;
    public LayerMask enemyLayer = 8;

    
    private float moveIntentionX = 0;
    private bool attemptJump = false;
    private bool meeleAttemptAttack = false;
    private bool rangeAttemptAtttack = false;
    private float timeUntillMeeleReadied = 0;
    private float timeUntilRangeReadied = 0;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleJump();
        HandleMeeleAttack();
        HandleRangeAttack();
    }

    void FixedUpdate() {
        HandleRun();
    }
    private void OnDrawGizmos() {
        Debug.DrawRay(transform.position, Vector2.up * groundedLeeway, Color.green);
        if(meeleAttackOrigin != null){
            Gizmos.DrawWireSphere(meeleAttackOrigin.position , meeleAttackRadius);
        }
    }

    private void HandleRun()
    {
        if(moveIntentionX > 0 && transform.rotation.y == 0){
            transform.rotation = Quaternion.Euler(0,180f,0);
        }else if(moveIntentionX < 0 && transform.rotation.y != 0){
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        Rb2D.velocity = new Vector2(moveIntentionX * speed,Rb2D.velocity.y);
    }
    private void HandleJump()
    {
        if(attemptJump && GroundCheck()){
            Rb2D.velocity = new Vector2(Rb2D.velocity.x, jumpForce);
        }
    }

    private void GetInput(){
        moveIntentionX = Input.GetAxis(xMoveAxis);
        meeleAttemptAttack = Input.GetKeyDown(meeleAttackKey);
        rangeAttemptAtttack = Input.GetKeyDown(rangeAttackKey);
        attemptJump = Input.GetKeyDown(jumpKey);
        
    }
    private void HandleMeeleAttack()
    {
        if(meeleAttemptAttack && timeUntillMeeleReadied <= 0){
            Debug.Log("attempting meele attack");
            Collider2D[] overlapedColliders = Physics2D.OverlapCircleAll(meeleAttackOrigin.position, meeleAttackRadius,enemyLayer);
            for(int i = 0;i < overlapedColliders.Length; i++) {
                IDamageAble enemyAttributes = overlapedColliders[i].GetComponent<IDamageAble>();
                if(enemyAttributes != null){
                    enemyAttributes.ApplyDamage(meeledamage);
                }
            }
            timeUntillMeeleReadied = meeleattackDelay;
        }else{
            timeUntillMeeleReadied -= Time.deltaTime;
        }
    }
    private void HandleRangeAttack(){
        if(rangeAttemptAtttack && timeUntilRangeReadied <= 0){
            Debug.Log("attempting range attack");
            Instantiate(Projectile,rangeAttackOrigin.position, rangeAttackOrigin.rotation);
            
            timeUntilRangeReadied = rangeAttackDelay;
        }else{
            timeUntilRangeReadied -= Time.deltaTime;
        }
    }

    public  void ApplyDamage(float amount){
        CurrentHealth -= amount;
        if(CurrentHealth <= 0){
            Debug.Log("get hit");
            Die();
        }
    }
    
}
