using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioforjump : MonoBehaviour
{
    public AudioClip jumpSound;
    private AudioSource audioSource;
    public CubeController cubeController;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cubeController.isGrounded && !cubeController.wonScreen.activeSelf && !cubeController.lostScreen.activeSelf && !cubeController.introScreen.activeSelf)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }
}
