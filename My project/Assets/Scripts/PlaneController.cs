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
    public Animator animator;
    private Rigidbody rb;
    private float responseModifier
    {
        get { return (rb.mass / 10f) * responsiveness; }
    }

    private void Awake()
    {
        GetComponent<Animator>().enabled = false;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        pitch = Input.GetAxis("Vertical") / 1f;
        roll = Input.GetAxis("Yaw");
        yaw = Input.GetAxis("Yaw");

        if(Input.GetKey(KeyCode.Space))
        {
            throttle += throttleIncrement;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            throttle -= throttleIncrement;
        }

        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * throttle * maxThrust);

        rb.AddTorque(transform.right * pitch * responseModifier * 3f);
        //rb.AddTorque(-transform.forward * roll * responseModifier);
        rb.AddTorque(transform.up * yaw * responseModifier * 6f);

        rb.AddForce(Vector3.up * rb.velocity.magnitude * lift);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Runway")
        {
            rb.velocity = Vector3.zero;
            throttle = 0f;
            pitch = 0f;
            roll = 0f;
            yaw = 0f;

            // reset the plane to the start position after 0.2 seconds
            transform.position = new Vector3(55.7099991f, 6.28000021f, -268.940002f);
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            StartCoroutine(ResPos());
            
        }
    }

    IEnumerator ResPos()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector3(55.7099991f, 6.28000021f, -268.940002f);
        transform.rotation = Quaternion.Euler(0f, -90f, 0f);
    }

}
