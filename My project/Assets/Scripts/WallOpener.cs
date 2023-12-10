using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOpener : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;

    public GameObject wall;

    private void Update()
    {
        //y position of button1
        float y = button1.transform.position.y;
        float y2 = button2.transform.position.y;
        if(y < -0.7f && y2 > 1.84f)
        {
            //lerp y pos of wall to 3.55f
            wall.transform.position = Vector3.Lerp(wall.transform.position, new Vector3(wall.transform.position.x, 3.55f, wall.transform.position.z), 1f * Time.deltaTime);
        }
    }
}
