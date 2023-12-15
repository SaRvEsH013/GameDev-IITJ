using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class change_scene_to_after_vol : MonoBehaviour
{
    private double time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 23)
        {
            SceneManager.LoadScene("tempRunner");
        }
    }
}
