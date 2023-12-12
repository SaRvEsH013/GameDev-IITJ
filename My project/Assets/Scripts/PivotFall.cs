using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotFall : MonoBehaviour
{
    public GameObject pivot;
    public GameObject p2;
    public GameObject button2;

    private void Update()
    {
        if (p2.transform.position.x >= button2.transform.position.x - 0.2f && p2.transform.position.x <= button2.transform.position.x + 0.2f && p2.transform.position.y + 0.65f >= button2.transform.position.y)
        {
            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, Quaternion.Euler(0, 0, 22), 1f * Time.deltaTime);
            button2.transform.position = Vector3.Lerp(button2.transform.position, new Vector3(button2.transform.position.x, 1.7f - 0.1f, button2.transform.position.z), 1f * Time.deltaTime);
        }
    }
}
