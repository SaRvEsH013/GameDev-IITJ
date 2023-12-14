using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    public float jumpForce = 4f;
    public float wallJumpForce = 5f; // Adjust the force for jumping off walls

    private Rigidbody rb;
    private bool isGrounded;
    private bool canWallJump;
    private bool canWall1Jump;
    private int lastJump = -1;
    public Animator animator;
    // Adjust this factor to control how fast the cube falls (opposite gravity)
    public float gravityFactor2;
    private string nameOfPlayer;
    private bool leftSided = false;
    public GameObject lostCanvas;

    void Start()
    {
        animator= GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        EnableControls(); // Enable controls by default
        nameOfPlayer = gameObject.name;
    }

    void Update()
    {
        // Handle player input for movement
        if (!enabled) return; // Don't process input if controls are disabled

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
            leftSided = true;

        }
        //if d pressed or left arrow pressed
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
            leftSided = false;
        }

        if (nameOfPlayer == "Man_Full (1)" && Input.GetKeyDown(KeyCode.P) && rb.velocity.y < 0.01 && rb.velocity.y > -0.1)
        {
            gravityFactor2 *= -1;
            if(gravityFactor2 > 0)
            {
                //use leftSided from MundiGhumao.cs
                if(leftSided)
                {
                    // move player 0.2f down
                    StartCoroutine(LerpPosition(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), 0.5f));
                    StartCoroutine(LerpRotation(Quaternion.Euler(180, 90, 180), 0.4f));
                }
                else
                {
                    // move player 0.2f up
                    StartCoroutine(LerpPosition(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), 0.5f));
                    StartCoroutine(LerpRotation(Quaternion.Euler(180, -90, 180), 0.4f));
                }
            }
            else
            {
                //use leftSided from MundiGhumao.cs
                if (leftSided)
                {
                    // move player 0.2f down
                    StartCoroutine(LerpPosition(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), 0.5f));
                    StartCoroutine(LerpRotation(Quaternion.Euler(180, 90, 0), 0.4f));
                }
                else
                {
                    // move player 0.2f up
                    StartCoroutine(LerpPosition(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), 0.5f));
                    StartCoroutine(LerpRotation(Quaternion.Euler(180, -90, 0), 0.4f));
                }
            }
        }

        // Get input from the keyboard
        float xInput = Input.GetAxis("Horizontal");

        if(xInput != 0f)
        {
            animator.SetBool("run",true);

        }
        else
        {
            animator.SetBool("run", false);
        }

        if(Input.GetButtonDown("Jump"))
        {
            animator.SetBool("jump", true);
        }
        else
        {
            animator.SetBool ("jump", false);
        }

        //print(xInput);
        // Move the cube continuously using the horizontal input
        rb.AddForce(new Vector3(xInput, 0, 0) * speed);

        // Limit the x speed of the cube
        float limitedSpeed = Mathf.Clamp(rb.velocity.x, -1f, 1f);
        rb.velocity = new Vector3(limitedSpeed, rb.velocity.y, rb.velocity.z);

        // Handle jumping when grounded or able to wall jump
        if ((isGrounded) && Input.GetButtonDown("Jump"))
        {
            Vector3 jumpDirection = isGrounded ? Vector3.up : Vector3.up + Vector3.forward; // Change the jump direction if jumping off a wall
            rb.AddForce(gravityFactor2 * (isGrounded ? jumpForce : wallJumpForce) * jumpDirection, ForceMode.Impulse);
            isGrounded = false; // Set to false when jumping
            canWallJump = false; // Reset wall jump flag
            canWall1Jump = false; // Reset wall jump flag
            lastJump = -1;
        }
        if((canWallJump) && Input.GetButtonDown("Jump") && lastJump!=0 && nameOfPlayer== "Man_Full")
        {
            Vector3 jumpDirection = isGrounded ? Vector3.up : Vector3.up + Vector3.forward; // Change the jump direction if jumping off a wall
            rb.AddForce(gravityFactor2 * (isGrounded ? jumpForce : wallJumpForce) * jumpDirection, ForceMode.Impulse);
            isGrounded = false; // Set to false when jumping
            canWallJump = false; // Reset wall jump flag
            canWall1Jump = false; // Reset wall jump flag
            lastJump = 0;
        }
        if ((canWall1Jump) && Input.GetButtonDown("Jump") && lastJump != 1 && nameOfPlayer == "Man_Full")
        {
            Vector3 jumpDirection = isGrounded ? Vector3.up : Vector3.up + Vector3.forward; // Change the jump direction if jumping off a wall
            rb.AddForce(gravityFactor2 * (isGrounded ? jumpForce : wallJumpForce) * jumpDirection, ForceMode.Impulse);
            isGrounded = false; // Set to false when jumping
            canWallJump = false; // Reset wall jump flag
            canWall1Jump = false; // Reset wall jump flag
            lastJump = 1;
        }
    }

    // Check if the player is grounded or in contact with a wall
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canWall1Jump = true;
            canWallJump = true;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            canWallJump = true;
        }
        else if (collision.gameObject.CompareTag("Wall1"))
        {
            canWall1Jump = true;
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

        // Property to check if the player is enabled
    public bool IsPlayerEnabled
    {
        get { return enabled; }
    }

    IEnumerator LerpRotation(Quaternion targetRotation, float duration)
    {
        //wait for 0.1 seconds
        float time = 0;
        Quaternion startRotation = transform.rotation;
        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
        //yield return new WaitForSeconds(0.5f);
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        //wait for 0.1 seconds
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        yield return new WaitForSeconds(0.5f);
    }

    //check trigger collision
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("lostGame"))
        {
            //print("lost game");
            //disable controls
            DisableControls();
            //show lost canvas
            lostCanvas.SetActive(true);
        }
    }
}
