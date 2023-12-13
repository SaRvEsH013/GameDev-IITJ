using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class for_explosion : MonoBehaviour
{
    [SerializeField] private GameObject explosionPlane;
    [SerializeField] private MonoBehaviour planeController;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetActiveExplosionPlane", 2f);
        explosionPlane.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to set active explosion_plane particle system
    public void SetActiveExplosionPlane()
    {
        if (!explosionPlane.activeSelf)
        {
            explosionPlane.SetActive(true);
            planeController.enabled = false; // Deactivate the plane controller script
        }
    }
}