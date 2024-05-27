using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Input")]
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode jumpKey = KeyCode.Space;
    public String xMoveAxis = "Horizontal";

    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 6f;
    public float groundedLeeway = 0.1f;

    private Rigidbody2D rb2D = null;
    private float moveIntentionX = 0;
    private bool attemptJump = false;
    private bool attemptAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Rigidbody2D>()){
            rb2D = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleJump();
        HandleAttack();
    }
    void FixedUpdate(){
        HandleRun();
    }
    private void GetInput(){
        moveIntentionX = Input.GetAxis(xMoveAxis);
        attemptAttack = Input.GetKeyDown(attackKey);
        attemptJump = Input.GetKeyDown(jumpKey);
    }
    private void HandleRun(){
        if(moveIntentionX > 0 && transform.rotation.y == 0){
            transform.rotation = Quaternion.Euler(0,180f,0);
        }else if(moveIntentionX < 0 && transform.rotation.y != 0){
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        rb2D.velocity = new Vector2(moveIntentionX * speed, rb2D.velocity.y);
    }
    private void HandleAttack(){

    }
    private void HandleJump(){

    }
}
