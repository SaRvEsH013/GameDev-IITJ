using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class volcano_script : MonoBehaviour
{
    public PlayableDirector director;
    private double time; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = director.time;
        if (time > 2.1)
        {
            print("ook");
            SceneManager.LoadScene("tempRunner");
        }
    }
}
