using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public GameObject eventMan;
    public GameObject introCan;
    public GameObject GameCan;

    public GameObject ball;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void OnStartClick()
    {
        introCan.SetActive(false);
        //if Gamecan is not null then set it to active
        if (GameCan != null)
        {
            GameCan.SetActive(true);
        }


        eventMan.GetComponent<GameScript>().enabled = true;
    }

    public void OnContinueClick()
    {
        // unload current scene async

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerVillageScript>().enabled = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<PlayerVillageScript>().missions[0] = true;
        player.GetComponent<PlayerVillageScript>().missionCount += 1;
        SceneManager.UnloadSceneAsync("RedGreenTestScene");

        // enable the player
        // find player by tag
    }

    public void onBackClick()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerVillageScript>().enabled = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        SceneManager.UnloadSceneAsync("RedGreenTestScene");
    }

    public void OnStartCubeJump()
    {
        introCan.SetActive(false);
    }

    public void onContinueCubeJump()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerVillageScript>().enabled = true;
        player.GetComponent<PlayerVillageScript>().missions[1] = true;
        player.GetComponent<PlayerVillageScript>().missionCount += 1;
        SceneManager.UnloadSceneAsync("Cube Jump");
    }

    public void onBackCubeJump()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<PlayerVillageScript>().enabled = true;
        SceneManager.UnloadSceneAsync("Cube Jump");
    }

    public void onStartMaze()
    {
        introCan.SetActive(false);
        ball.SetActive(true);
    }

    public void onContinueMaze()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerVillageScript>().enabled = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<PlayerVillageScript>().missions[2] = true;
        player.GetComponent<PlayerVillageScript>().missionCount += 1;
        SceneManager.UnloadSceneAsync("Maze");
    }

    public void onBackMaze()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerVillageScript>().enabled = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        SceneManager.UnloadSceneAsync("Maze");
    }
}
