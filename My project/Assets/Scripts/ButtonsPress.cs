using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsPress : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;

    public GameObject button1;
    public GameObject button2;

    public bool done1 = false;
    public bool done2 = false;

    private void Update()
    {
        //if y button1 is already -0.72 and y button2 is already 1.84, do nothing
        if(done1 && done2)
        {
            return;
        }

        //if p1 is in contact with button1, make y pos of button1 to -0.72 smoothly
        if(p1.transform.position.x >= button1.transform.position.x - 0.2f && p1.transform.position.x <= button1.transform.position.x + 0.2f && p1.transform.position.y <= button1.transform.position.y+0.35f)
        {
            //button1.transform.position = Vector3.Lerp(button1.transform.position, new Vector3(button1.transform.position.x, -0.72f, button1.transform.position.z), 2f * Time.deltaTime);
            StartCoroutine(LerpPosition(new Vector3(button1.transform.position.x, -0.72f, button1.transform.position.z), 3f));
            done1 = true;
        }

        //if p2 is in contact with button2, make y pos of button2 to 1.84 smoothly
        if (p2.transform.position.x >= button2.transform.position.x - 0.2f && p2.transform.position.x <= button2.transform.position.x + 0.2f && p2.transform.position.y+0.35f >= button2.transform.position.y)
        {
            //button2.transform.position = Vector3.Lerp(button2.transform.position, new Vector3(button2.transform.position.x, 1.86f, button2.transform.position.z), 2f * Time.deltaTime); 
            StartCoroutine(LerpPosition2(new Vector3(button2.transform.position.x, 1.88f, button2.transform.position.z), 3f));
            done2 = true;
            //print("HUA");
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = button1.transform.position;
        while (time < duration)
        {
            button1.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        button1.transform.position = targetPosition;
    }

    IEnumerator LerpPosition2(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = button2.transform.position;
        while (time < duration)
        {
            button2.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        button2.transform.position = targetPosition;
    }
}
