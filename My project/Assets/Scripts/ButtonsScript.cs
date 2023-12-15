using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public GameObject EscMenu;
    public GameObject eventMan;
    public GameObject introCan;
    public GameObject winCan;
    public GameObject lossCan;
    public GameObject GameCan;
    public GameObject fadeCan;
    public Image fadeImage;

    public AudioClip mazeStart;
    public AudioClip lava;

    public GameObject ball;
    void Start()
    {
        StartCoroutine(Fade(true));

        if (SceneManager.GetActiveScene().name == "Maze") AudioSource.PlayClipAtPoint(mazeStart, transform.position);
        if (SceneManager.GetActiveScene().name == "Cube Jump") AudioSource.PlayClipAtPoint(lava, transform.position, 0.5f);
    }


    void Update()
    {
        if ((introCan == null ||introCan.activeSelf == false) && (lossCan == null || lossCan.activeSelf == false) && (winCan == null || winCan.activeSelf == false))
        {
            Global_Script.global_time += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EscMenu.activeSelf == false)
            {
                Time.timeScale = 0f;
                EscMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                EscMenu.SetActive(false);
            }
        }
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
        SceneManager.LoadScene("Airport taking off");
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
        SceneManager.LoadScene("Volcano_after_cutscene");
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
        StartCoroutine(Fade(false));
        Invoke("LoadPlaneAnimation", 1f);
        //load plane initial animation and then load red green test scene
    }

    public void onBackMaze()
    {
        StartCoroutine(Fade(false));
        Invoke("LoadOffice", 1f);
    }

    void LoadOffice()
    {
        SceneManager.LoadScene("Office_Scene");
    }
    void LoadPlaneAnimation()
    {
        SceneManager.LoadScene("Airport_Cutscene");
    }

    public void onBackATO()
    {
        SceneManager.LoadScene("Airport taking off");
    }

    public void onQuitEsc()
    {
        Application.Quit();
    }
    public void onBackEsc()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnStartTempRunner()
    {
        introCan.SetActive(false);
        //if Gamecan is not null then set it to active
        if (GameCan != null)
        {
            GameCan.SetActive(true);
        }
    }

    public void OnRestartTempRunner()
    {
        SceneManager.LoadScene("tempRunner");
    }
    public void onResumeEsc()
    {
        Time.timeScale = 1f;
        EscMenu.SetActive(false);
    }
    IEnumerator Fade(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
            fadeImage.canvas.sortingOrder = -1;
        }
        else
        {
            fadeImage.canvas.sortingOrder = 10;
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
}
