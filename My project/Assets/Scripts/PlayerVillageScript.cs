using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVillageScript : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    public Animator animator;
    public bool[] missions = new bool[2];
    public int missionCount = 0;
    public GameObject finalStart;
    public GameObject plane;
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
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYPos = playerCamera.transform.localPosition.y;
        
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        animator = GetComponent<Animator>();
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
        print(ShouldJump);
        // check if final start is enabled
        if (!finalStart.activeSelf && missionCount == missions.Length)
        {
            finalStart.SetActive(true);
        }        
        
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();


            if (canJump)
                HandleJump();

            if (canCrouch)
                //HandleCrouch();

            if (canUseHeadbob)
                HandleHeadbob();

            ApplyFinalMovements();

        }
        //Llamada al Salto
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

    //Si saltamos
    private void HandleJump()
    {

        if (ShouldJump)
            moveDirection.y = jumpForce;
    }

    //Si nos agachamos
    private void HandleCrouch()
    {
        if (ShouldCrouch)
            StartCoroutine(CrouchStand());
    }

    //Movimiento de la cabeza(cámara) dependiendo de nuestro estado
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

    //Movimiento y gravedad incluyendo si nuestro jugador esta o no esta pisando
    private void ApplyFinalMovements()
    {

        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

    }

    //Al agacharse identifica si tenemos un objeto encima para no atravesarlo si nos erguimos
    //Al agacharse se realiza suavemente y viceversa
    //Al agacharse no podemos correr y saltar
    private IEnumerator CrouchStand()
    {
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break;

        duringCrouchAnimation = true;


        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
        canJump = !canJump;
        canSprint = !canSprint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "RedGreen" && missions[0] == false)
        {
            gameObject.GetComponent<PlayerVillageScript>().enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
            SceneManager.LoadScene("RedGreenTestScene", LoadSceneMode.Additive);
        }
        if (collision.gameObject.tag == "CubeJump" && missions[1] == false)
        {
            gameObject.GetComponent<PlayerVillageScript>().enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
            SceneManager.LoadScene("CubeJump", LoadSceneMode.Additive);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "RedGreen" && missions[0] == false)
        {
            gameObject.GetComponent<PlayerVillageScript>().enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            SceneManager.LoadScene("RedGreenTestScene", LoadSceneMode.Additive);
        }
        if (hit.gameObject.tag == "CubeJump" && missions[1] == false)
        {
            gameObject.GetComponent<PlayerVillageScript>().enabled = false;
            transform.position = new Vector3(transform.position.x + 2, transform.position.y + 1, transform.position.z);
            SceneManager.LoadScene("Cube Jump", LoadSceneMode.Additive);
        }
        if(hit.gameObject.tag == "FinalStart")
        {
            plane.gameObject.SetActive(true);
            this.enabled = false;
        }
    }

}
