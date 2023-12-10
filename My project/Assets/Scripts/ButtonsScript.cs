using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void OnContinueClick()
    {
        // unload current scene async

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerVillageScript>().enabled = true;
        SceneManager.UnloadSceneAsync("RedGreenTestScene");

        // enable the player
        // find player by tag
    }
}
