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
        isGrounded = IsGrounded();

        float horizontalInput = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    private float groundCheckDistance = 0.1f; // Adjust this value as needed

    private bool IsGrounded()
    {
        Bounds bounds = boxCollider.bounds;
        Vector2 bottomCenter = new Vector2(bounds.center.x, bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(bottomCenter, Vector2.down, groundCheckDistance, whatIsGround);

        return hit.collider != null;
    }

}
