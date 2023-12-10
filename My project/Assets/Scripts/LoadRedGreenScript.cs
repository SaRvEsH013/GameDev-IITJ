using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadRedGreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = new Vector3(other.transform.position.x, transform.position.y, transform.position.z + 2);

        // load the red/green scene and save the current scene
        SceneManager.LoadScene("RedGreenTestScene", LoadSceneMode.Additive);

        // disbale the player unitl the red/green scene is done
        other.gameObject.GetComponent<PlayerVillageScript>().enabled = false;


    }
}
