using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backToMain : MonoBehaviour
{
    // Start is called before the first frame update
    public void backToMainScene()
    {
        SceneManager.LoadScene("game_starting");
    }
}
