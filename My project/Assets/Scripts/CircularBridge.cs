using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularBridge : MonoBehaviour
{
    public GameObject circularBridge;
    public GameObject p1;
    public GameObject button2;
    private bool done = false;

    private void Update()
    {
        //if(done) return;
        if (p1.transform.position.x >= button2.transform.position.x - 0.2f && p1.transform.position.x <= button2.transform.position.x + 0.2f && p1.transform.position.y <= button2.transform.position.y + 0.45f)
        {
            done = true;
            StartCoroutine(LerpPosition(new Vector3(button2.transform.position.x, 6.513f - 0.1f, button2.transform.position.z), 5f));
            StartCoroutine(LerpRotation(Quaternion.Euler(0, 0, 90), 5f));
        }
    }

    //IEnumerator DoIt()
    //{
        //for(float i = 0; i < 500; i += Time.deltaTime)
        //{
        //    circularBridge.transform.rotation = Quaternion.Lerp(circularBridge.transform.rotation, Quaternion.Euler(0, 0, 90), i/1000);
        //}
        //for(float i = 0; i < 1000; i += Time.deltaTime)
        //{
        //    button2.transform.position = Vector3.Lerp(button2.transform.position, new Vector3(button2.transform.position.x, 6.513f - 0.1f, button2.transform.position.z), i/1000);
       // }
       //
       // button2.transform.position = Vector3.SmoothDamp(button2.transform.position, new Vector3(button2.transform.position.x, 6.513f - 0.1f, button2.transform.position.z), ref Vector3.zero, 1f);
//
     //   yield return null;
    //}

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    IEnumerator LerpRotation(Quaternion targetRotation, float duration)
    {
        float time = 0;
        Quaternion startRotation = transform.rotation;
        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
    }

}
