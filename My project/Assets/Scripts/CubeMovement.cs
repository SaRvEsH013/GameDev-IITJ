using UnityEngine;

public class CubeController : MonoBehaviour
{
    public GameObject introScreen;
    public GameObject wonScreen;
    public GameObject lostScreen;
    public GameObject cube;
    public float moveSpeed = 200f; // Speed of the cuboid
    //Space button should apply a force in y and z direction to the cube
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //check if intro screen is active
            if (!introScreen.activeSelf && !lostScreen.activeSelf && !wonScreen.activeSelf)
            {
                cube.GetComponent<Rigidbody>().AddForce(0, moveSpeed, -1f*moveSpeed);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //tag if TargetCube then show won screen
        if (collision.gameObject.CompareTag("TargetCube") && !lostScreen.activeSelf)
        {
            wonScreen.SetActive(true);
        }
        //tag if ObstacleCube then show lost screen
        if (collision.gameObject.CompareTag("LostCube") && !wonScreen.activeSelf)
        {
            lostScreen.SetActive(true);
        }
    }
}
