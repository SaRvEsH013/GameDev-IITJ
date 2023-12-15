using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_scene : MonoBehaviour
{
    public double time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 31.0f)
        {
            SceneManager.LoadScene("03 - Islands");
        }
    }
}
