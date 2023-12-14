using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerVillageScript : MonoBehaviour
{
    public Animator animator;
    public bool CanMove { get; private set; } = true;

    public bool[] missions = new bool[3];
    public int missionCount = 0;
    public GameObject plane;
    public Image fadeImage;
    //private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    //private bool ShouldJump => Input.GetKeyDown(jumpkey) && characterController.isGrounded;
    //private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && characterController.isGrounded;

/*    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = false;
    [SerializeField] private bool canUseHeadbob = true;*/

/*    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpkey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;*/

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

/*    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 15.0f;
    [SerializeField] private float gravity = 30.0f;*/

/*    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;*/

/*    [Header("Headbob Parameters")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.11f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos = 0;
    private float timer;*/

    //private Vector3 hitPointNormal;
    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;
    private float rotationY = 0;

/*    [Header("Footsteps objects")]
    AudioSource audioSource;


    [Header("Etiqueta Madera")]
    public AudioClip[] Pasosmadera;
    //public Texture madera;

    [Header("Etiqueta Pasto")]
    public AudioClip[] Pasospasto;
    //public Texture pasto;

    [Header("Etiqueta Piedra")]
    public AudioClip[] Pasospiedra;
    //public Texture piedra;*/

    [Header("Intervalo de pasos")]
    public float TimeBetweenSteps;
    float tiempo;
    int soundControl;
    public bool isMoving;
    public bool isSpriting;
    public bool isAgachado;
    float airTime;

    void Awake()
    {
        StartCoroutine(Fade(true));

        animator = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();

        //audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < missions.Length; i++)
        {
            missions[i] = false;
        }

    }

    //Los movimientos son identificados
    void Update()
    {

        animator.SetBool("Run", isMoving);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0)
        {
            isMoving = true;
            transform.Translate(Vector3.right * h * walkSpeed * Time.deltaTime);

        }
        if (v != 0)
        {
            isMoving = true;
            transform.Translate(Vector3.forward * v * walkSpeed * Time.deltaTime);
        }
        if (h == 0 && v == 0)
        {
            isMoving = false;
        }

        rotationX = Input.GetAxis("Mouse X") * lookSpeedX;
        transform.Rotate(0, rotationX, 0);
/*
        rotationY = Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationY = Mathf.Clamp(rotationY, -upperLookLimit, lowerLookLimit);
        transform.Rotate(0, 0, -rotationY);*/

        // if no mouse input, stop rotating
        if (Input.GetAxis("Mouse X") == 0)
        {
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
/*        if (Input.GetAxis("Mouse Y") == 0)
        {
            GetComponent<Rigidbody>().angularVelocity.x.Equals(0);
        }*/

    }
    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Begin"))
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(Fade(false));
            //freeze player position

            // load "office_scene" scene after 1 second delay 
            Invoke(nameof(LoadOfficeScene), 1f);
        }

        if (hit.gameObject.tag == "RedGreen" && missions[0] == false)
        {
            if (SceneManager.GetSceneByName("RedGreenTestScene").isLoaded) return;

            StartCoroutine(Fade(false));
            transform.position = new Vector3(transform.position.x + 2, transform.position.y + 1, transform.position.z);
            gameObject.GetComponent<PlayerVillageScript>().enabled = false;

            SceneManager.LoadScene("RedGreenTestScene", LoadSceneMode.Additive);
        }
        if (hit.gameObject.tag == "CubeJump" && missions[1] == false)
        {
            if (SceneManager.GetSceneByName("Cube Jump").isLoaded) return;

            StartCoroutine(Fade(false));
            transform.position = new Vector3(transform.position.x + 2, transform.position.y + 2, transform.position.z);
            gameObject.GetComponent<PlayerVillageScript>().enabled = false;


            SceneManager.LoadScene("Cube Jump", LoadSceneMode.Additive);
        }
        if (hit.gameObject.tag == "Maze" && missions[2] == false)
        {
            if (SceneManager.GetSceneByName("Maze").isLoaded) return;

            StartCoroutine(Fade(false));
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Invoke("LoadMazeScene", 1f);
        }
        if (hit.gameObject.tag == "FinalStart")
        {
            plane.gameObject.SetActive(true);
            this.enabled = false;
        }
    }



    private void LoadOfficeScene()
    {
        SceneManager.LoadScene("Office_Scene");
    }
    private void LoadMazeScene()
    {
        SceneManager.LoadScene("Maze");
    }

    IEnumerator Fade(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }


}