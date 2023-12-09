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

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // Handle jumping only when grounded
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce * gravityFactor, ForceMode.Impulse);
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