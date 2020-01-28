using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
     
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpSpeed = 7f;
    [SerializeField] float climbSpeed = 3f;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private LayerMask layerMask;
    private CapsuleCollider2D bodyCollider;
    private BoxCollider2D feetCollider;

    private bool isAlive = true;
    private bool isPlayerClimbing = false;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        layerMask = LayerMask.GetMask("Foreground");
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isAlive)
        {
            Move();
            Jump();
            Climb();
            Death();
        }
        
    }

    private void Climb()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            float climb = Input.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(rigidbody.velocity.x, climb * climbSpeed);
            rigidbody.velocity = climbVelocity;
            rigidbody.gravityScale = 0f;
            isPlayerClimbing = Mathf.Abs(rigidbody.velocity.y) > Mathf.Epsilon;
            animator.SetBool("climbing", isPlayerClimbing);

        }
        else
        {
            animator.SetBool("climbing", false);
            rigidbody.gravityScale = 1f;
        }
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && feetCollider.IsTouchingLayers(layerMask))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rigidbody.velocity += jumpVelocityToAdd;
             
        }
    }

    private void Move()
    {
        var newPosX = Input.GetAxis("Horizontal") *  moveSpeed;
        Vector2 playerVelocity = new Vector2(newPosX * moveSpeed, rigidbody.velocity.y);
        rigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        
        if (playerHasHorizontalSpeed)
        {
            animator.SetBool("running", playerHasHorizontalSpeed);
            transform.localScale = new Vector2(Mathf.Sign(rigidbody.velocity.x), 1f);
        }
    }
    private void Death()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards","Enemy")))
        {
           
            isAlive = false;
            animator.SetTrigger("death");
            GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 5f);
        }
    }
     
}
