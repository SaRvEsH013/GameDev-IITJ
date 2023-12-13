using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Animation_plane_script : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    public Animator animator;
    void Start()
    {
        SceneManager.LoadScene("03 - Islands");

        SceneManager.LoadScene("03 - Islands");

        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        
       
            //load scene Islands
            SceneManager.LoadScene("03 - Islands");
        
        
    }
    void OnCollisionEnter(Collision collision)
    {
        
            //load scene Islands
            SceneManager.LoadScene("Islands");

        
    }
}
