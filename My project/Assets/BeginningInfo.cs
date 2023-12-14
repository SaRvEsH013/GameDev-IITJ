using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BeginningInfo : MonoBehaviour
{
    private double temp_Time = 0f ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        temp_Time += Time.deltaTime;
        if (temp_Time > 5f )
        {
            this.gameObject.SetActive( false );
        }
    }
}
