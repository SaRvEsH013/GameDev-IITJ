using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_change : MonoBehaviour
{
    // Start is called before the first frame update
    public void change_scene(string scene_name)
    {
        SceneManager.LoadSceneAsync(scene_name);
    }
}
