using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MundiGhumao2 : MonoBehaviour
{
    private bool leftSided = true;
    private void Update()
    {
        //check if playerController is enabled or not
        if (!GetComponent<PlayerController>().enabled)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!leftSided)
            {
                transform.Rotate(0, 180, 0);
                leftSided = true;
            }
        }
        //if d pressed
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (leftSided)
            {
                transform.Rotate(0, 180, 0);
                leftSided = false;
            }
        }
    }
}
