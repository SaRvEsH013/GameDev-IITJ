using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotFall : MonoBehaviour
{
    public GameObject pivot;
    public GameObject p2;
    public GameObject button2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "DefaultMalePBR")
        {
            button2.transform.position = Vector3.Lerp(button2.transform.position, new Vector3(button2.transform.position.x, button2.transform.position.y - 0.1f, button2.transform.position.z), 1f * Time.deltaTime);
            _ = new WaitForSeconds(1f);
            //call function to rotate pivot 22 degrees on z axis smoothly
            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, Quaternion.Euler(0, 0, 22), 1f * Time.deltaTime);
        }
    }
}
