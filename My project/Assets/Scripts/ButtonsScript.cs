using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    public GameObject eventMan;
    public GameObject introCan;
    public GameObject GameCan;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void OnStartClick()
    {
        introCan.SetActive(false);
        GameCan.SetActive(true);
        eventMan.GetComponent<GameScript>().enabled = true;
    }
}
