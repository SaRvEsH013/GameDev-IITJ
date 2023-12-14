using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class Global_Script : MonoBehaviour
{
    public static float global_time = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // set text as time in canvas
        this.gameObject.GetComponent<TMP_Text>().text = Math.Round(global_time).ToString();
        
    }
}
