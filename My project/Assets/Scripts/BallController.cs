//using GLTF.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject wonCan;
    public GameObject lostCan;

    private bool isMoving = false;
    private Vector3 curVel = Vector3.zero;
    private bool won;
    private bool lost;
        
    public AudioClip mazeLost, mazeWon;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // move with curr velocity
        transform.position += curVel * Time.deltaTime;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        print(curVel);

        if (x != 0)
        {
            curVel = new Vector3(x, 0, 0) * speed;
        }
        else if (z != 0)
        {
            curVel = new Vector3(0, 0, z) * speed;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "TargetCube" && !lost)
        {
            //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            wonCan.SetActive(true);
            won = true;
            AudioSource.PlayClipAtPoint(mazeWon, transform.position);
        }
        else if(!won)
        {
            lostCan.SetActive(true);
            lost = true;
            AudioSource.PlayClipAtPoint(mazeLost, transform.position);
        }
        //else if(collision.gameObject.tag != "TargetCube")
        //{
        //    lostCan.SetActive(true);

        //}
    }

   
}
