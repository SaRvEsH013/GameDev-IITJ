using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlaneController : MonoBehaviour
{
    public float throttleIncrement = 0.1f;
    public float maxThrust = 200f;
    public float responsiveness = 1f;
    public float lift = 135f;
    public int count = 0;

    private float throttle;
    private float pitch;
    private float roll;
    private float yaw;
    public Animator animator;
    public Rigidbody rb;
    public Camera cam;
    public Camera cam2;
    public Image fadeImage;
    public GameObject lostCan;

    public AudioClip airportBack;
    public AudioClip cylinder;
    private float responseModifier
    {
        get { return (rb.mass / 10f) * responsiveness; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        throttle = 0f;
        pitch = 0f;
        roll = 0f;
        yaw = 0f;

        if(SceneManager.GetActiveScene().name == "Airport_Cutscene")
        {
            AudioSource.PlayClipAtPoint(airportBack, transform.position, 4f);
        }
    }

    private void Update()
    {
        if(count == 2)
        {
            StartCoroutine(Fade(false));
            Invoke("LoadVolcano", 1f);
        }

        pitch = Input.GetAxis("Vertical") / 1f;
        roll = Input.GetAxis("Yaw");
        yaw = Input.GetAxis("Yaw");

        //enable camera and disable camera 2 
        //How to get time 
        
        if ( transform.position.z <-9.3 && !SceneManager.GetSceneByName("RedGreenTestScene").isLoaded && SceneManager.GetActiveScene().name  != "Airport taking off")
        {
            SceneManager.LoadScene("RedGreenTestScene");
        }


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
    void LoadVolcano()
    {
        SceneManager.LoadScene("Volcano");
    }


    private void FixedUpdate()
    {
        rb.AddForce(maxThrust * throttle * transform.forward);

        rb.AddTorque(20f * pitch * responseModifier * transform.right);
        //rb.AddTorque(-transform.forward * roll * responseModifier);
        rb.AddTorque(25f * responseModifier * yaw * transform.up);

        rb.AddForce(lift * rb.velocity.magnitude * Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "VolcanoStart")
        {
            AudioSource.PlayClipAtPoint(cylinder, transform.position, 0.7f);
            count++;
            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Runway")
        {
            lostCan.SetActive(true);
            this.GetComponent<PlaneController>().enabled = false;
        }
    }

    /*    private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Runway"))
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
        }*/

    /*IEnumerator ResPos()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector3(55.7099991f, 6.28000021f, -268.940002f);
        transform.rotation = Quaternion.Euler(0f, -90f, 0f);
    }*/

    IEnumerator Fade(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }

}
