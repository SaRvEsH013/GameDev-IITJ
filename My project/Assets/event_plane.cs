using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class event_plane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -9)
        {
            SceneManager.LoadScene("03 - Islands");
        }
    }
}
