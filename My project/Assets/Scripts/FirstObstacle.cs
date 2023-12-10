using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstObstacle : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;

    public GameObject obstacle1;
    public GameObject obstacle2;

    private void Update()
    {
        float maximumX = Mathf.Max(p1.transform.position.x, p2.transform.position.x);
        if(maximumX >= obstacle1.transform.position.x)
        {
            obstacle1.transform.position = Vector3.Lerp(obstacle1.transform.position, new Vector3(obstacle1.transform.position.x, -0.15f, obstacle1.transform.position.z), 0.2f * Time.deltaTime);
            obstacle2.transform.position = Vector3.Lerp(obstacle2.transform.position, new Vector3(obstacle2.transform.position.x, 1.23f, obstacle2.transform.position.z), 0.2f * Time.deltaTime);
        }
    }
}
