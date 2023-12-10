using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVillageScript : MonoBehaviour
{
    public int speed = 15;
    void Start()
    {
        
    }

    void Update()
    {
        // Move the player using the arrow keys

        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 18;
        }
        else
        {
            speed = 10;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        // Rotate the player using the mouse
        transform.Rotate(0, Input.GetAxis("Mouse X") * 2, 0);

        // jump using space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 4, 0), ForceMode.Impulse);
        }
    }
}
