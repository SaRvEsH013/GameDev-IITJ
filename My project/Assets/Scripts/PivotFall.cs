using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotFall : MonoBehaviour
{
    public GameObject pivot;
    public GameObject p2;
    public GameObject button2;
    private bool done = false;

    private void Update()
    {
        if (p2.transform.position.x >= button2.transform.position.x - 0.2f && p2.transform.position.x <= button2.transform.position.x + 0.2f && p2.transform.position.y + 0.65f >= button2.transform.position.y)
        {
            /*pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, Quaternion.Euler(0, 0, 22), 1f * Time.deltaTime);
            button2.transform.position = Vector3.Lerp(button2.transform.position, new Vector3(button2.transform.position.x, 1.7f - 0.1f, button2.transform.position.z), 1f * Time.deltaTime);*/
            done = true;
            StartCoroutine(LerpPosition(new Vector3(button2.transform.position.x, 1.7f - 0.1f, button2.transform.position.z), 5f));
            StartCoroutine(LerpRotation(Quaternion.Euler(0, 0, 22), 5f));
        }
    }

    IEnumerator LerpRotation(Quaternion targetRotation, float duration)
    {
        float time = 0;
        Quaternion startRotation = pivot.transform.rotation;
        while (time < duration)
        {
            pivot.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        pivot.transform.rotation = targetRotation;
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
}
