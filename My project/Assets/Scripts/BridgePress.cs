using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePress : MonoBehaviour
{

    public GameObject bridge1;
    public GameObject bridge2;

    public GameObject p2;
    public GameObject button;

    private void Update()
    {
        //if p2 is in contact with button,make y pos of bridge1 and bridge2 decrease by 1.325f smoothly
        if (p2.transform.position.x >= button.transform.position.x - 0.2f && p2.transform.position.x <= button.transform.position.x + 0.2f && p2.transform.position.y +0.35f >= button.transform.position.y)
        {
            bridge1.transform.position = Vector3.Lerp(bridge1.transform.position, new Vector3(bridge1.transform.position.x, bridge1.transform.position.y - 0.625f, bridge1.transform.position.z), 1f * Time.deltaTime);
            bridge2.transform.position = Vector3.Lerp(bridge2.transform.position, new Vector3(bridge2.transform.position.x, bridge2.transform.position.y - 0.625f, bridge2.transform.position.z), 1f * Time.deltaTime);
            //increase y pos of button by 0.1f
            button.transform.position = Vector3.Lerp(button.transform.position, new Vector3(button.transform.position.x, button.transform.position.y + 0.1f, button.transform.position.z), 1f * Time.deltaTime);
        }
    }
}
