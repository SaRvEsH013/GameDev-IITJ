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

        // fly plane  and man enters volcano animation and load cube jump scene

    }

    public void onBackClick()
    {
        SceneManager.LoadScene("RedGreenTestScene");
    }

    public void OnStartCubeJump()
    {
        introCan.SetActive(false);
    }

    public void onContinueCubeJump()
    {
        SceneManager.LoadScene("tempRunner");
    }

    public void onBackCubeJump()
    {
        SceneManager.LoadScene("Cube Jump");
    }

    public void onStartMaze()
    {
        introCan.SetActive(false);
        ball.SetActive(true);
    }

    public void onContinueMaze()
    {
        //load plane initial animation and then load red green test scene
    }

    public void onBackMaze()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerVillageScript>().enabled = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        SceneManager.UnloadSceneAsync("Maze");
    }
}
