using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak : MonoBehaviour
{
    public GameObject wall;
    public GameObject p1;
    public GameObject button1;

    private void Update()
    {
        //if wall is already destroyed, do nothings

        //if p1 is on button1, destroy wall
        if (p1.transform.position.x >= button1.transform.position.x - 0.2f && p1.transform.position.x <= button1.transform.position.x + 0.2f && p1.transform.position.y <= button1.transform.position.y + 0.35f)
        {
            button1.transform.position = Vector3.Lerp(button1.transform.position, new Vector3(button1.transform.position.x, button1.transform.position.y - 0.1f, button1.transform.position.z), 1f * Time.deltaTime);
            //destroy wall after 1 second
            Destroy(wall, 1f);
        }
    }
}
