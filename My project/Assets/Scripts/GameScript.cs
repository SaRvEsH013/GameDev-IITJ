using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    public GameObject leftGreen, rightGreen, leftRed, rightRed, green_explosion_left, green_explosion_right, red_explosion_left, red_explosion_right;
    public GameObject wonCanvas, lostCanvas;
    public int maxScore = 5;
    public int winScore = 2;

    int score = 0;
    float tempTime = 0;
    int left;
    int clicked = 0;
    int count = 0;
    public AudioClip audioClip;

    void Start()
    {

        leftGreen.SetActive(false);
        rightGreen.SetActive(false);
        leftRed.SetActive(false);
        rightRed.SetActive(false);
        green_explosion_left.SetActive(false);
        green_explosion_right.SetActive(false);
        red_explosion_left.SetActive(false);
        red_explosion_right.SetActive(false);
        wonCanvas.SetActive(false);
        lostCanvas.SetActive(false);

        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    void Update()
    {
        if (count >= maxScore)
        {
            leftGreen.SetActive(false);
            rightGreen.SetActive(false);
            leftRed.SetActive(false);
            rightRed.SetActive(false);

            if (score >= winScore)
            {
                wonCanvas.SetActive(true);
            }
            else
            {
                lostCanvas.SetActive(true);
            }
            return;
        }
        tempTime += Time.deltaTime;

        if (tempTime < 0.7)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && left == 1 && clicked == 0)
            {
                score++;
                scoreText.text = "Score: " + score.ToString();
                clicked = 1;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && left == 0 && clicked == 0)
            {
                score++;
                scoreText.text = "Score: " + score.ToString();
                clicked = 1;
            }
        }
        if (tempTime > 1)
        {
            leftGreen.SetActive(false);
            rightGreen.SetActive(false);
            leftRed.SetActive(false);
            rightRed.SetActive(false);
            green_explosion_left.SetActive(false);
            green_explosion_right.SetActive(false);
            red_explosion_right.SetActive(false);
            red_explosion_left.SetActive(false);

            if (tempTime < 1.05 || count >= maxScore) return;

            left = Random.Range(0, 2);
            int green;
            if (left == 0)
            {
                green = Random.Range(0, 2);
                if (green == 0)
                {
                    leftRed.SetActive(true);
                    red_explosion_left.SetActive(true);
                }
                else
                {
                    rightGreen.SetActive(true);
                    green_explosion_right.SetActive(true);
                }
            }
            else
            {
                green = Random.Range(0, 2);
                if (green == 0)
                {
                    rightRed.SetActive(true);
                    red_explosion_right.SetActive(true);

                }
                else
                {
                    leftGreen.SetActive(true);
                    green_explosion_left.SetActive(true);

                }
            }
            PlayAudio();
            tempTime = 0;
            clicked = 0;
            count++;

        }
    }
    void PlayAudio()
    {
        //AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }
}
