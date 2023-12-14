using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseCircularBridge : MonoBehaviour
{
    public GameObject circularBridge;
    public GameObject p2;
    public GameObject button2;
    public GameObject cam;
    private bool done = false;
    public PlayerController pl1;
    public PlayerController pl2;

    private void Update()
    {
        if(done) return;
        if (p2.transform.position.x >= button2.transform.position.x - 0.2f && p2.transform.position.x <= button2.transform.position.x + 0.2f && p2.transform.position.y + 0.45f >= button2.transform.position.y)
        {
/*            circularBridge.transform.rotation = Quaternion.Lerp(circularBridge.transform.rotation, Quaternion.Euler(0, 0, 0), 0.5f * Time.deltaTime);
            print(button2.transform.position.y);
            button2.transform.position = Vector3.Lerp(button2.transform.position, new Vector3(button2.transform.position.x, 4.78f + 0.1f, button2.transform.position.z), 1f * Time.deltaTime);*/
            done = true;
            //disable controls for both players
            pl1.DisableControls();
            pl2.DisableControls();
            //move camera to the center of the bridge
            StartCoroutine(LerpPosition1(new Vector3(circularBridge.transform.position.x, circularBridge.transform.position.y, circularBridge.transform.position.z - 6f), 0.5f));
            StartCoroutine(LerpPosition(new Vector3(button2.transform.position.x, 4.78f + 0.1f, button2.transform.position.z), 5f));
            StartCoroutine(LerpRotation(Quaternion.Euler(0, 0, 0), 5f));

            //enable controls for both players after 5 seconds
            StartCoroutine(EnableControlsAfter(5f));
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

    IEnumerator LerpPosition1(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = cam.transform.position;
        while (time < duration)
        {
            cam.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        cam.transform.position = targetPosition;
    }

    IEnumerator EnableControlsAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        pl2.EnableControls();
    }
}
