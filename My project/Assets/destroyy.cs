using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyy : MonoBehaviour
{
    float tp = 0f;

    // Update is called once per frame
    void Update()
    {
        tp += Time.deltaTime;
        if (tp > 4f)
        {
            Destroy(this.gameObject);
        }
    }
}
