using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    private bool isRunning => canRun && Input.GetKey(RunKey) && !isCrouching;
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !DuringCourchingAnimation && characterController.isGrounded;
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded && !isCrouching;

    [SerializeField] CharacterController characterController;
    [SerializeField] PlayerDataSO PlayerDataSO;
    internal Animator PlayerAnimator;

    public Camera playerCamera;
    private float rotationX = 0;

    [Header("Camera_Look System")]
    [SerializeField, Range(1, 10)] private float lookspeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookspeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLooklimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLooklimit = 50.0f;

    [Header("Movement System")]
    private Vector3 movedirection;
    private Vector2 currentInput;

    [Header("HeadBob System")]
    [SerializeField] private float walkBobSpeed = 12f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float runBobSpeed = 14f;
    [SerializeField] private float runBobAmount = 0.1f;
    [SerializeField] private float crouchBobSpeed = 7f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos = 0;
    private float timer;

    [Header("Funtion Option")]
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canRun = true;
    [SerializeField] private bool canuseHeadbob = true;

    [Header("control System ")]
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode RunKey = KeyCode.LeftShift;

    [Header("Jumping System")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 10.0f;

    [Header("Crouching System ")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 1.6f;
    [SerializeField] private float timetoCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 StandingCenter = new Vector3(0, 0, 0);


    private PlayerController playerControl;
    [SerializeField] private float tickTime;
    public bool canMove { get; private set; } = true;
    private bool isCrouching;
    private bool DuringCourchingAnimation;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerControl = new PlayerController();
        PlayerAnimator = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<Camera>();
        defaultYPos = playerCamera.transform.localPosition.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {

        if (PlayerStatusManager.Instance.playerdataSo.CurrentEnegy <= 0)
        {
            canRun = false;
        }
        else
        {
            canRun = true;
        }



        if (Input.GetMouseButton(1) && GlobalReferences.Instance.CheckIsCloseSystem()
            && !QuestManager.Instance.isQuestMenuOpen
            && !PlacementSystem.Instance.inPlacementMode
            && !StorageManager.Instance.storageUIOpen)
        {

            PlayerAnimator.SetBool("isADSCamera", true);
        }
        else
        {

            PlayerAnimator.SetBool("isADSCamera", false);
        }

        if (canMove  && !StorageManager.Instance.storageUIOpen)
        {
            handleMovement();
            handleMouseLook();

            if (canCrouch)
            {

                handleCrouch();
            }
            if (canJump)
            {

                handleJump();
            }
            if (canuseHeadbob)
            {
                handleHeadBob();
            }
            FinalApplyMovement();
        }

        //---------------------- FunEffect to Status --------------
        MinusEnegyWhenRun();
    }


    //---------------------- Status - + ------------------------
    private void MinusEnegyWhenRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            tickTime -= Time.deltaTime;
            if (tickTime < 0 && PlayerStatusManager.Instance.isDead == false)
            {
                PlayerStatusManager.Instance.MinusEnegy(5);
                tickTime = 3.0f;
            }

        }
    }

    //---------------------- Movement Funt ------------------------

    private void handleHeadBob()
    {
        if (!characterController.isGrounded) { return; }
        if (Mathf.Abs(movedirection.x) > 0.1f || Mathf.Abs(movedirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : isRunning ? runBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) *
                (isCrouching ? crouchBobAmount : isRunning ? runBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z);
        }
    }
    private void handleMouseLook()
    {
        if (!InventorySystem.Instance.isInventoryOpen
            && !CraftingManager.Instance.IsCraftOpen
            && !DialogManager.Instance.isDiablogUIActive
            && !QuestManager.Instance.isQuestMenuOpen
           && !StorageManager.Instance.storageUIOpen)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookspeedY;
            rotationX = Mathf.Clamp(rotationX, -upperLooklimit, lowerLooklimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookspeedX, 0);
        }
    }

    private void handleMovement()
    {
        currentInput = new Vector2((isRunning ? PlayerDataSO.RunSpeed : isCrouching ? PlayerDataSO.CrouchSpeed : PlayerDataSO.WalkSpeed) * Input.GetAxis("Vertical"),
        (isRunning ? PlayerDataSO.RunSpeed : isCrouching ? PlayerDataSO.CrouchSpeed : PlayerDataSO.WalkSpeed) * Input.GetAxis("Horizontal"));


        float moveDirectionY = movedirection.y;
        movedirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) +
        (transform.TransformDirection(Vector3.right) * currentInput.y);
        movedirection.y = moveDirectionY;

    }
    private void FinalApplyMovement()
    {
        if (!characterController.isGrounded)
        {
            movedirection.y -= gravity * Time.deltaTime;
        }
        characterController.Move(movedirection * Time.deltaTime);

    }

    private void handleJump()
    {
        if (ShouldJump)
        {
            (this).movedirection.y = jumpForce;

        }
    }
    private void handleCrouch()
    {
        if (ShouldCrouch)
        {
            StartCoroutine(CrouchStand());

        }
    }
    private IEnumerator CrouchStand()
    {

        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
        {
            yield break;
        }
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break;
        DuringCourchingAnimation = true;
        float timeELapse = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? StandingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;
        while (timeELapse < timetoCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeELapse / timetoCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeELapse / timetoCrouch);
            timeELapse += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;
        isCrouching = !isCrouching;
        DuringCourchingAnimation = false;
    }

    private void OnEnable()
    {
        playerControl?.Enable();
    }

    private void OnDisable()
    {
        playerControl?.Disable();
    }

    //************************************************************************************
    //private void moveDirection()
    //{
    //    float hInput = Input.GetAxisRaw("Horizontal"); 
    //    float vInput = Input.GetAxisRaw("Vertical");
    //    Vector3 moveDirection = (transform.right * hInput * 2) + (transform.forward * vInput * PlayerDataSO.WalkSpeed);
    //    moveDirection.y = transform.position.y;
    //    Debug.Log("===== direction : " + moveDirection);
    //    characterController.SimpleMove(moveDirection);
    //    if (Input.GetKey(KeyCode.LeftShift))
    //    {
    //        Vector3 RunDirection = transform.right * hInput + transform.forward * vInput;
    //        characterController.SimpleMove(RunDirection * PlayerDataSO.RunSpeed);
    //    }
    //}
}
