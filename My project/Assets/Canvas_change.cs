using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_change : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canvas_change = false;
    private Canvas CanvasPlayerGUI;
    void Start()
    {
        
    }
    public void Bool_Change(bool canva)
    {
        canvas_change = canva;
    }
    // Update is called once per frame
    void Update()
    {
        GameObject CanvasPlayerGUI = GameObject.Find("Canvas");
        if (canvas_change)
        {
            CanvasPlayerGUI.SetActive(false);

        }
    }

    
    
}
