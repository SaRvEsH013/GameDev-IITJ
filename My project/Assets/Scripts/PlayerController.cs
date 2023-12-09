using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    public float jumpForce = 4f;

    private Rigidbody rb;
    private bool isGrounded;

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

        // Get input from the keyboard
        float xInput = Input.GetAxis("Horizontal");
        
        // Move the cube continuously using the horizontal and vertical inputs
        rb.AddForce(new Vector3(xInput, 0, 0) * speed);
        //limit the x speed of the cube
        if (rb.velocity.x > 1f)
        {
            rb.velocity = new Vector3(1f, rb.velocity.y, rb.velocity.z);
        }
        else if (rb.velocity.x < -1f)
        {
            rb.velocity = new Vector3(-1f, rb.velocity.y, rb.velocity.z);
        }

        // Handle jumping only when grounded
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(gravityFactor * jumpForce * Vector3.up, ForceMode.Impulse);
            isGrounded = false; // Set to false when jumping
        }
    }

    // Check if the player is grounded
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
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