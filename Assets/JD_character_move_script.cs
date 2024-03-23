using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JD_character_move_script : MonoBehaviour
{
    public float speed = 7.0f;
    public float jumpForce = 10f; // The force applied for jumps
    public Animator animator;
    public LayerMask whatIsGround; // LayerMask to specify what layers constitute 'ground'
    public Transform groundCheck; // The point around which to check for ground
    public float groundCheckRadius = 0.05f; // The radius to check around the ground point

    private Rigidbody2D rb;
    private bool isGrounded; // Flag to determine if the character is grounded
    private BoxCollider2D boxCollider; // Reference to the BoxCollider2D component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component from the game object
    }

    void Update()
    {
        // Check if the character is on the ground
        isGrounded = IsGrounded();

        // Get input from the horizontal axis (A and D keys, left and right arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Update the animator with the movement speed
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Flip the character sprite based on the direction
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }

        // Jumping
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Use FixedUpdate for Physics-based updates
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    private float groundCheckDistance = 0.1f; // Adjust this value as needed

    private bool IsGrounded()
    {
        // Bounds of the BoxCollider2D
        Bounds bounds = boxCollider.bounds;
        // Bottom center of the collider
        Vector2 bottomCenter = new Vector2(bounds.center.x, bounds.min.y);
        // Check for ground slightly below the bottom center of the collider
        RaycastHit2D hit = Physics2D.Raycast(bottomCenter, Vector2.down, groundCheckDistance, whatIsGround);

        // If the raycast hits something on the ground layer, the character is grounded
        return hit.collider != null;
    }

}
