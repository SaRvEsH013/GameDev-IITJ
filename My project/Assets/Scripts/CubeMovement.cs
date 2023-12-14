using UnityEngine;

public class CubeController : MonoBehaviour
{
    public GameObject introScreen;
    public GameObject wonScreen;
    public GameObject lostScreen;
    public GameObject cube;
    public bool isGrounded;
    public float moveSpeed = 200f; // Speed of the cuboid
    //Space button should apply a force in y and z direction to the cube
    public bool hasStarted = false;
    public AudioClip lavaDeath;
    public Animator animator;
    

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space) && !hasStarted)
        {
            hasStarted = true;
            introScreen.SetActive(false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //check if intro screen is active
            if (!introScreen.activeSelf && !lostScreen.activeSelf && !wonScreen.activeSelf)
            {
                cube.GetComponent<Rigidbody>().AddForce(0, moveSpeed, -1f*moveSpeed);
                isGrounded = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("jump", true);
        }
        else
        {
            animator.SetBool("jump", false);

        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        //tag if TargetCube then show won screen
        if (collision.gameObject.CompareTag("TargetCube") && !lostScreen.activeSelf)
        {
            wonScreen.SetActive(true);
        }
        //tag if ObstacleCube then show lost screen
        if (collision.gameObject.CompareTag("LostCube") && !wonScreen.activeSelf)
        {
            AudioSource.PlayClipAtPoint(lavaDeath, transform.position);
            lostScreen.SetActive(true);
        }
    }
}
