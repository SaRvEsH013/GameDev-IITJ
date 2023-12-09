using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsPress : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;

    public GameObject button1;
    public GameObject button2;

    private void Update()
    {
        //if y button1 is already -0.72 and y button2 is already 1.84, do nothing
        if(button1.transform.position.y <= -0.72f && button2.transform.position.y >= 1.84f)
        {
            return;
        }

        //if p1 is in contact with button1, make y pos of button1 to -0.72 smoothly
        if(p1.transform.position.x >= button1.transform.position.x - 0.2f && p1.transform.position.x <= button1.transform.position.x + 0.2f)
        {
            button1.transform.position = Vector3.Lerp(button1.transform.position, new Vector3(button1.transform.position.x, -0.72f, button1.transform.position.z), 2f * Time.deltaTime);
        }

        //if p2 is in contact with button2, make y pos of button2 to 1.84 smoothly
        if (p2.transform.position.x >= button2.transform.position.x - 0.2f && p2.transform.position.x <= button2.transform.position.x + 0.2f)
        {
            button2.transform.position = Vector3.Lerp(button2.transform.position, new Vector3(button2.transform.position.x, 1.84f, button2.transform.position.z), 2f * Time.deltaTime);
        }
    }
}
