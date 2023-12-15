using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class input_name : MonoBehaviour
{
    public string name1;
    public void SaveName(string newName)
    {
        name1 = newName;
        Debug.Log(name1);
    }

}