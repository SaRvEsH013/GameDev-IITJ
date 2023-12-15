using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadtemprunner : MonoBehaviour
{
    float temp = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        temp += Time.deltaTime;
        if (temp > 31f)
        {
            SceneManager.LoadScene("tempRunner");
        }
    }
}
