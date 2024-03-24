using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f; // Speed of player movement
    public float jumpForce = 10f; // Force of player jump

    public Transform groundCheck; // A transform at the player's feet to check if they're grounded
    public LayerMask groundLayer; // The layer(s) representing the ground
    public Transform ceilCheck;

    private Rigidbody2D rb;
    private bool isGrounded;

    private Animator animator;

    private float timeFalling = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);


        // Player movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if(moveInput < 0)
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveInput > 0)
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        animator.SetBool("isJumping", !isGrounded);
        animator.SetBool("isFalling", rb.velocity.y < 0);
        if(rb.velocity.y < 0 && !isGrounded)
        {
            timeFalling += Time.deltaTime;
        }
        else
        {
            timeFalling = 0;
        }
        if(timeFalling > 6f)
        {
            Die();
        }

        // Player jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce + rb.velocity.y);
        }


        bool getsHead = Physics2D.OverlapCircle(ceilCheck.position, 0.01f, groundLayer);
        if(getsHead && isGrounded)
        {
            Die();
        }
    }

    public void Die()
    {
        GameMaster.Instance.Die();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obj")
        {
            GameMaster.Instance.RestartLevel();
        }
    }
}
