using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partical_sound : MonoBehaviour
{
    public AudioSource audioSource; // Declare an AudioSource variable
    public AudioClip explosion; // Declare an AudioClip variable
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(explosion, new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
