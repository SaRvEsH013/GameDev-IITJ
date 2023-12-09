using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float throttleIncrement = 0.1f;
    public float maxThrust = 200f;
    public float responsiveness = 1f;
    public float lift = 135f;

    private float throttle;
    private float pitch;
    private float roll;
    private float yaw;

    private Rigidbody rb;
    private float responseModifier
    {
        get { return (rb.mass / 10f) * responsiveness; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        pitch = Input.GetAxis("Vertical");
        roll = Input.GetAxis("Horizontal");
        yaw = Input.GetAxis("Yaw");

        if(Input.GetKey(KeyCode.Space))
        {
            throttle += throttleIncrement;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            throttle -= throttleIncrement;
        }

        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * throttle * maxThrust);

        rb.AddTorque(transform.right * pitch * responseModifier);
        rb.AddTorque(-transform.forward * roll * responseModifier);
        rb.AddTorque(transform.up * yaw * responseModifier);

        rb.AddForce(Vector3.up * rb.velocity.magnitude * lift);
    }
}
