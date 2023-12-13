using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularBridge : MonoBehaviour
{
    public GameObject circularBridge;
    public GameObject p1;
    public GameObject button2;

    private void Update()
    {
        if (p1.transform.position.x >= button2.transform.position.x - 0.2f && p1.transform.position.x <= button2.transform.position.x + 0.2f && p1.transform.position.y <= button2.transform.position.y + 0.45f)
        {
            circularBridge.transform.rotation = Quaternion.Lerp(circularBridge.transform.rotation, Quaternion.Euler(0, 0, 90), 0.5f * Time.deltaTime);
            //print(button2.transform.position.y);
            button2.transform.position = Vector3.Lerp(button2.transform.position, new Vector3(button2.transform.position.x, 6.513f - 0.1f, button2.transform.position.z), 1f * Time.deltaTime);
        }
    }
}
