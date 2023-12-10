using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    public float jumpForce = 4f;
    public float wallJumpForce = 5f; // Adjust the force for jumping off walls

    private Rigidbody rb;
    private bool isGrounded;
    private bool canWallJump;

    // Adjust this factor to control how fast the cube falls (opposite gravity)
    public float gravityFactor;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        EnableControls(); // Enable controls by default
    }

    void Update()
    {
        // Handle player input for movement
        if (!enabled) return; // Don't process input if controls are disabled

        // Get input from the keyboard
        float xInput = Input.GetAxis("Horizontal");

        // Move the cube continuously using the horizontal input
        rb.AddForce(new Vector3(xInput, 0, 0) * speed);

        // Limit the x speed of the cube
        float limitedSpeed = Mathf.Clamp(rb.velocity.x, -1f, 1f);
        rb.velocity = new Vector3(limitedSpeed, rb.velocity.y, rb.velocity.z);

        print(isGrounded + " " + canWallJump);

        // Handle jumping when grounded or able to wall jump
        if ((isGrounded || canWallJump) && Input.GetButtonDown("Jump"))
        {
            Vector3 jumpDirection = isGrounded ? Vector3.up : Vector3.up + Vector3.forward; // Change the jump direction if jumping off a wall
            rb.AddForce(gravityFactor * (isGrounded ? jumpForce : wallJumpForce) * jumpDirection, ForceMode.Impulse);
            isGrounded = false; // Set to false when jumping
            canWallJump = false; // Reset wall jump flag
        }
    }

    // Check if the player is grounded or in contact with a wall
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            canWallJump = true;
        }
    }

    // Enable controls and set grounded to true when enabled
    public void EnableControls()
    {
        enabled = true;
    }

    public void DisableControls()
    {
        enabled = false;
    }
}
