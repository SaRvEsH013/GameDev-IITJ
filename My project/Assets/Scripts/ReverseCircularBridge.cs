using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseCircularBridge : MonoBehaviour
{
    public GameObject circularBridge;
    public GameObject p2;
    public GameObject button2;

    private void Update()
    {
        print(p2.transform.position.x + " " +  button2.transform.position.x);
        if (p2.transform.position.x >= button2.transform.position.x - 0.2f && p2.transform.position.x <= button2.transform.position.x + 0.2f && p2.transform.position.y + 0.45f >= button2.transform.position.y)
        {
            circularBridge.transform.rotation = Quaternion.Lerp(circularBridge.transform.rotation, Quaternion.Euler(0, 0, 0), 0.5f * Time.deltaTime);
            print(button2.transform.position.y);
            button2.transform.position = Vector3.Lerp(button2.transform.position, new Vector3(button2.transform.position.x, 4.78f + 0.1f, button2.transform.position.z), 1f * Time.deltaTime);
        }
    }
}
