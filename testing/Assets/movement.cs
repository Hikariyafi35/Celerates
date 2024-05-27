using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded = false;
    public Transform groundCheck;
    public float checkRadius = 0.1f;
    public LayerMask ground;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);

        float moveDirection = Input.GetAxis("Horizontal");

        // Move right
        if (moveDirection > 0)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            if (!facingRight)
                Flip();
        }
        // Move left
        else if (moveDirection < 0)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            if (facingRight)
                Flip();
        }
        // Stop moving horizontally
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("lompat");
        }
    }

    // Function to flip the character's direction
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
