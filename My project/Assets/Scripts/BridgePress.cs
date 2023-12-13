using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePress : MonoBehaviour
{

    public GameObject bridge1;
    public GameObject bridge2;

    public GameObject p2;
    public GameObject button;

    private bool done = false;

    private void Update()
    {
        if (done) return;
        //if p2 is in contact with button,make y pos of bridge1 and bridge2 decrease by 1.325f smoothly
        if (p2.transform.position.x >= button.transform.position.x - 0.2f && p2.transform.position.x <= button.transform.position.x + 0.2f && p2.transform.position.y +0.35f >= button.transform.position.y)
        {
            //bridge1.transform.position = Vector3.Lerp(bridge1.transform.position, new Vector3(bridge1.transform.position.x, bridge1.transform.position.y - 0.625f, bridge1.transform.position.z), 1f * Time.deltaTime);
            //bridge2.transform.position = Vector3.Lerp(bridge2.transform.position, new Vector3(bridge2.transform.position.x, bridge2.transform.position.y - 0.625f, bridge2.transform.position.z), 1f * Time.deltaTime);
            //increase y pos of button by 0.1f
            //button.transform.position = Vector3.Lerp(button.transform.position, new Vector3(button.transform.position.x, -4.6f + 0.1f, button.transform.position.z), 1f * Time.deltaTime);

            done = true;
            StartCoroutine(LerpPosition(new Vector3(button.transform.position.x, button.transform.position.y + 0.1f, button.transform.position.z), 2f));
            StartCoroutine(LerpPosition2(new Vector3(bridge1.transform.position.x, bridge1.transform.position.y - 0.625f, bridge1.transform.position.z), 3f));
            StartCoroutine(LerpPosition3(new Vector3(bridge2.transform.position.x, bridge2.transform.position.y - 0.625f, bridge2.transform.position.z), 3f));
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = button.transform.position;
        while (time < duration)
        {
            button.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        button.transform.position = targetPosition;
    }

    IEnumerator LerpPosition2(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = bridge1.transform.position;
        while (time < duration)
        {
            bridge1.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        bridge1.transform.position = targetPosition;
    }

    IEnumerator LerpPosition3(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = bridge2.transform.position;
        while (time < duration)
        {
            bridge2.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        bridge2.transform.position = targetPosition;
    }
}
