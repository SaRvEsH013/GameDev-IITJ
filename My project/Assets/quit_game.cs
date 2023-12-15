using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quit_game : MonoBehaviour
{
    // Start is called before the first frame update

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}