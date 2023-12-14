using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;
    public Camera mainCamera;
    public float switchSpeed = 5f; // Adjust the speed of the camera switch

    private PlayerController p1Controller;
    private PlayerController p2Controller;

    public Animator pa1;
    public Animator pa2;

    void Start()
    {
        // Ensure that p1, p2, and mainCamera are assigned in the Unity Editor.
        if (p1 == null || p2 == null || mainCamera == null)
        {
            Debug.LogError("Assign the player GameObjects (p1 and p2) and the main camera in the Unity Editor.");
            return;
        }

        // Get the PlayerController scripts from the player GameObjects.
        p1Controller = p1.GetComponent<PlayerController>();
        p2Controller = p2.GetComponent<PlayerController>();

        // Enable controls for p1 and disable controls for p2 at the start.
        p1Controller.EnableControls();
        p2Controller.DisableControls();
    }

    void Update()
    {
        //if both disabled, do nothing
        if (!p1Controller.enabled && !p2Controller.enabled)
        {
            return;
        }
        // If the 'K' key is pressed, switch between p1 and p2 controls.
        if (Input.GetKeyDown(KeyCode.K))
        {
            SwitchCharacter();
        }

        // Update the camera's position to smoothly follow the selected character's position.
        Vector3 targetPosition = p1Controller.enabled ? p1.transform.position : p2.transform.position;

        float distOfY = Mathf.Abs(p1.transform.position.y - p2.transform.position.y)/2;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(targetPosition.x, (p1.transform.position.y + p2.transform.position.y+0.5f) / 2f, Mathf.Min(p1.transform.position.z - distOfY - 1f, p1.transform.position.z -  2.5f)), switchSpeed * Time.deltaTime);
        
    }

    void SwitchCharacter()
    {
        if (p1Controller.enabled)
        {
            pa1.Rebind();
            pa1.Update(0f);
            p1Controller.DisableControls();
            p2Controller.EnableControls();
        }
        else
        {
            pa2.Rebind();
            pa2.Update(0f);
            p1Controller.EnableControls();
            p2Controller.DisableControls();
        }
    }

}
