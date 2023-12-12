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
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => Input.GetKeyDown(jumpkey) && characterController.isGrounded;
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && characterController.isGrounded;

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = false;
    [SerializeField] private bool canUseHeadbob = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpkey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;

    [Header("Headbob Parameters")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.11f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos = 0;
    private float timer;

    //private Vector3 hitPointNormal;
    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    [Header("Footsteps objects")]
    AudioSource audioSource;


    [Header("Etiqueta Madera")]
    public AudioClip[] Pasosmadera;
    //public Texture madera;

    [Header("Etiqueta Pasto")]
    public AudioClip[] Pasospasto;
    //public Texture pasto;

    [Header("Etiqueta Piedra")]
    public AudioClip[] Pasospiedra;
    //public Texture piedra;

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
        defaultYPos = playerCamera.transform.localPosition.y;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        audioSource = GetComponent<AudioSource>();

        // set missions to false
        for (int i = 0; i < missions.Length; i++)
        {
            missions[i] = false;
        }

    }

    //Identifica la etiqueta para reproducir un sonido
    /*private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.transform.tag)
        {
            case "Madera":
                soundControl = 0;
                break;
            case "Pasto":
                soundControl = 1;
                break;
            case "Piedra":
                soundControl = 2;
                break;
        }
    }*/

    //Los movimientos son identificados
    void Update()
    {

        animator.SetBool("Jump", ShouldJump);
        animator.SetBool("Run", isMoving);
        animator.SetBool("Sprint", isSpriting);

        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();


            if (canJump)
                HandleJump();

            if (canUseHeadbob)
                HandleHeadbob();

            ApplyFinalMovements();

        }
        PlaySoundFalling();

        //Condiciones para los sonidos de pasos(footsteps objects)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0 && characterController.isGrounded)
        {
            isMoving = true;
            tiempo -= Time.deltaTime;
            if (tiempo <= 0)
            {
                /*switch (soundControl)
                {
                    case 0: audioSource.clip = Pasosmadera[Random.Range(0, Pasosmadera.Length)]; break;
                    case 1: audioSource.clip = Pasospasto[Random.Range(0, Pasospasto.Length)]; break;
                    case 2: audioSource.clip = Pasospiedra[Random.Range(0, Pasospiedra.Length)]; break;
                }*/
                tiempo = TimeBetweenSteps;
                /*audioSource.pitch = Random.Range(0.65f, 1f);
                audioSource.volume = Random.Range(0.85f, 1f);
                audioSource.Play();*/
            }
        }
        else
        {
            isMoving = false;
            tiempo = Time.deltaTime;
        }

        if (IsSprinting)
        {
            isSpriting = true;
            TimeBetweenSteps = 0.5f;
        }
        else
        {
            isSpriting = false;
            TimeBetweenSteps = 1f;
        }

        if (isCrouching)
        {
            isAgachado = true;
            TimeBetweenSteps = 1.5f;
        }
        else
        {
            isAgachado = false;
        }

        //Sonido del salto solo al entrar en contacto con el objeto
        void PlaySoundFalling()
        {
            if (!characterController.isGrounded)
            {
                airTime += Time.deltaTime;
            }
            else
            {
                if (airTime > 0.2f)
                {
                    /* switch (soundControl)
                     {
                         case 0: audioSource.clip = Pasosmadera[Random.Range(0, Pasosmadera.Length)]; break;
                         case 1: audioSource.clip = Pasospasto[Random.Range(0, Pasospasto.Length)]; break;
                         case 2: audioSource.clip = Pasospiedra[Random.Range(0, Pasospiedra.Length)]; break;
                     }*/
                    tiempo = TimeBetweenSteps;
                    /*audioSource.pitch = Random.Range(0.65f, 0.70f);
                    audioSource.volume = Random.Range(0.65f, 0.75f);
                    audioSource.Play();*/
                    airTime = 0;
                }
            }

        }

    }

    //Se detecta los botones del teclado para ejecutar acciones
    private void HandleMovementInput()
    {

        currentInput = new Vector2((IsSprinting ? sprintSpeed : isCrouching ? crouchSpeed : walkSpeed) * Input.GetAxis("Vertical"), (IsSprinting ? sprintSpeed : isCrouching ? crouchSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;

    }

    //Movimiento del Mouse
    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);

    }

    private void HandleJump()
    {
        if (ShouldJump)
            moveDirection.y = jumpForce;
    }

    private void HandleHeadbob()
    {
        if (!characterController.isGrounded) return;

        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(
                 playerCamera.transform.localPosition.x,
                 defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
                 playerCamera.transform.localPosition.z);
        }
    }

    private void ApplyFinalMovements()
    {

        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Begin")
        {
            StartCoroutine(Fade(false));
            SceneManager.LoadScene("Office_Scene");
        }

        if (hit.gameObject.tag == "RedGreen" && missions[0] == false)
        {
            if (SceneManager.GetSceneByName("RedGreenTestScene").isLoaded) return;

            StartCoroutine(Fade(false));
            transform.position = new Vector3(transform.position.x + 2, transform.position.y + 1, transform.position.z);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            gameObject.GetComponent<PlayerVillageScript>().enabled = false;

            SceneManager.LoadScene("RedGreenTestScene", LoadSceneMode.Additive);
        }
        if (hit.gameObject.tag == "CubeJump" && missions[1] == false)
        {
            if (SceneManager.GetSceneByName("Cube Jump").isLoaded) return;

            StartCoroutine(Fade(false));
            transform.position = new Vector3(transform.position.x + 2, transform.position.y + 2, transform.position.z);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            gameObject.GetComponent<PlayerVillageScript>().enabled = false;

            SceneManager.LoadScene("Cube Jump", LoadSceneMode.Additive);
        }
        if (hit.gameObject.tag == "Maze" && missions[2] == false)
        {
            if (SceneManager.GetSceneByName("Maze").isLoaded) return;

            StartCoroutine(Fade(false));
            transform.position = new Vector3(transform.position.x + 2, transform.position.y + 1, transform.position.z);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            gameObject.GetComponent<PlayerVillageScript>().enabled = false;

            SceneManager.LoadScene("Maze", LoadSceneMode.Additive);
        }
        if (hit.gameObject.tag == "FinalStart")
        {
            plane.gameObject.SetActive(true);
            this.enabled = false;
        }
    }

    IEnumerator Fade(bool fadeAway)
    {
        if(fadeAway)
        {
            for(float i = 1; i >= 0; i -= Time.deltaTime)
            {
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        else
        {
            for(float i = 0; i <= 1; i += Time.deltaTime)
            {
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
    

}
