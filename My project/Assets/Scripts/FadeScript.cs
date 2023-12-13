using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public Image fadeImage;
    void Start()
    {
        //StartCoroutine(Fade(true));
    }


    void Update()
    {
        
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
            // get the canvas of fade image and set it to false
            fadeImage.canvas.enabled = false;
        }
        else
        {
            fadeImage.canvas.enabled = true;
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
}
