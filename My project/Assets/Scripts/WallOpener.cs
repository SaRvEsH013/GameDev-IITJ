using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOpener : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject buttonPress;

    public GameObject wall;
    private bool done = false;
    //value of bool done1 and done2 is taken from ButtonsPress.cs
    private bool done1 = false;
    private bool done2 = false;


    private void Update()
    {
        if(done) return;
        done1 = buttonPress.GetComponent<ButtonsPress>().done1;
        done2 = buttonPress.GetComponent<ButtonsPress>().done2;
        if(done1 && done2)
        {
            //lerp y pos of wall to 3.55f
            //wall.transform.position = Vector3.Lerp(wall.transform.position, new Vector3(wall.transform.position.x, 3.55f, wall.transform.position.z), 1f * Time.deltaTime);
            done = true;
            StartCoroutine(LerpPosition(new Vector3(wall.transform.position.x, 3.55f, wall.transform.position.z), 5f));
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = wall.transform.position;
        while (time < duration)
        {
            wall.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        wall.transform.position = targetPosition;
    }   
}
