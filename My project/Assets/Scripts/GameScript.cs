using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TextMeshProUGUI scoreText;
    public GameObject leftGreen, rightGreen, leftRed, rightRed;
    int score = 0;
    float tempTime = 0;
    int left;
    int clicked = 0;

    void Blink()
    {
        int left = Random.Range(0, 2);
        if(left == 0)
        {
            int green = Random.Range(0, 2);
            if(green == 0)
            {
                leftRed.SetActive(true);
            }
            else
            {
                leftGreen.SetActive(true);
            }
        }
        else
        {
            int green = Random.Range(0, 2);
            if (green == 0)
            {
                rightRed.SetActive(true);
            }
            else
            {
                rightGreen.SetActive(true);
            }
        }
    }


    void Start()
    {
        // set all lights off at start
        leftGreen.SetActive(false);
        rightGreen.SetActive(false);
        leftRed.SetActive(false);
        rightRed.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        tempTime += Time.deltaTime;

        if (tempTime < 1)
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

            if (tempTime < 2) return;

            left = Random.Range(0, 2);
            int green;
            if (left == 0)
            {
                green = Random.Range(0, 2);
                if (green == 0)
                {
                    leftRed.SetActive(true);
                }
                else
                {
                    rightGreen.SetActive(true);
                }
            }
            else
            {
                green = Random.Range(0, 2);
                if (green == 0)
                {
                    rightRed.SetActive(true);
                }
                else
                {
                    leftGreen.SetActive(true);
                }
            }

            tempTime = 0;
            clicked = 0;
            
        }
    }
}
