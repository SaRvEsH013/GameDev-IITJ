using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f;

    public GameObject wonCan;
    public GameObject lostCan;
        
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get input from vercal axis and horizontal axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // move the ball continuously in the direction of the input
        if(x != 0 || z != 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(x * speed, 0, z * speed);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "TargetCube")
        {
            wonCan.SetActive(true);

        }
    }
}
