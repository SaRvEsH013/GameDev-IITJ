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
    public GameObject GameCan;
    public GameObject fadeCan;
    public Image fadeImage;

    public GameObject ball;
    void Start()
    {
        StartCoroutine(Fade(true));
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(EscMenu.activeSelf == false)
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
        SceneManager.LoadScene(3);
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
