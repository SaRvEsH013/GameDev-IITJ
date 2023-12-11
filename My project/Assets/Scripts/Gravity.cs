using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody rb;
    public float gravityFactor;
    public bool allowReversal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (allowReversal && Input.GetKeyDown(KeyCode.P))
        {
            //check if grounded
            if(rb.velocity.y < 0.01 && rb.velocity.y > -0.1)
            {
                gravityFactor *= -1;
                print("Gravity Reversed");
            }
        }
    }

    void FixedUpdate()
    {
        
        rb.AddForce(gravityFactor * Physics.gravity.y * Vector3.up, ForceMode.Acceleration);
    }
}
