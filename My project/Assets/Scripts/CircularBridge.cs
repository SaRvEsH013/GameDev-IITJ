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
        if(done) return;
        if (p1.transform.position.x >= button2.transform.position.x - 0.2f && p1.transform.position.x <= button2.transform.position.x + 0.2f && p1.transform.position.y <= button2.transform.position.y + 0.45f)
        {
            done = true;
            StartCoroutine(LerpPosition(new Vector3(button2.transform.position.x, 6.513f - 0.1f, button2.transform.position.z), 5f));
            StartCoroutine(LerpRotation(Quaternion.Euler(0, 0, -90), 5f));
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
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

    IEnumerator LerpRotation(Quaternion targetRotation, float duration)
    {
        float time = 0;
        Quaternion startRotation = circularBridge.transform.rotation;
        while (time < duration)
        {
            circularBridge.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        circularBridge.transform.rotation = targetRotation;
    }

}
