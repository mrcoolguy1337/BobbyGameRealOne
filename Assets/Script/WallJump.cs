using UnityEngine;

public class WallJump : MonoBehaviour
{
    // The force applied to the player when they jump off a wall
    public float jumpForce = 9f;

    // The number of times the player can jump off a wall before touching the ground again
    public int wallJumpsAllowed = 1;

    // The layer that the player's collider should check for wall collisions
    public LayerMask wallLayer;

    // A reference to the player's rigidbody component
    private Rigidbody2D rb;

    // A counter for the number of wall jumps the player has performed
    private int wallJumps = 0;

    void Start()
    {
        // Get a reference to the player's rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is pressing the jump button and they are colliding with a wall
        if (Input.GetButtonDown("Jump") && IsTouchingWall())
        {
            // Increment the wall jump counter
            wallJumps++;

            // If the player has not exceeded the maximum number of wall jumps allowed, jump off the wall
            if (wallJumps <= wallJumpsAllowed)
            {
                // Apply the jump force to the player's rigidbody
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player touches the ground, reset the wall jump counter
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            wallJumps = 0;
        }
    }

    bool IsTouchingWall()
    {
        // Check if the player's collider is colliding with any colliders on the wall layer
        return Physics2D.OverlapCircle(transform.position, 0.1f, wallLayer);
    }
}
