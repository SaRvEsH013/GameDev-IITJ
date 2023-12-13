using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak : MonoBehaviour
{
    public GameObject wall;
    public GameObject p1;
    public GameObject button1;
    private bool done = false;

    private void Update()
    {
        //if wall is already destroyed, do nothings
        if (done) return;
        //if p1 is on button1, destroy wall
        if (p1.transform.position.x >= button1.transform.position.x - 0.2f && p1.transform.position.x <= button1.transform.position.x + 0.2f && p1.transform.position.y <= button1.transform.position.y + 0.35f)
        {
            //button1.transform.position = Vector3.Lerp(button1.transform.position, new Vector3(button1.transform.position.x, button1.transform.position.y - 0.1f, button1.transform.position.z), 1f * Time.deltaTime);
            //destroy wall after 1 second
            done = true;
            StartCoroutine(LerpPosition(new Vector3(button1.transform.position.x, 1.7f - 0.1f, button1.transform.position.z), 1f));
            Destroy(wall, 1f);
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
}
