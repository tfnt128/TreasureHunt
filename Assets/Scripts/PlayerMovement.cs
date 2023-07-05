using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float gravity;

    [Header("Look")]
    [SerializeField] private float lookSpeedX;
    [SerializeField] private float lookSpeedY;
    [SerializeField] private float upperLookLimit;
    [SerializeField] private float lowerLookLimit;

    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 forward;
    private Vector3 right;
    private Vector3 moveDirection;
    public Vector2 currentInput;

    private float rotationX = 0;

    public bool isDead;

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();

       // LockCursor(CursorLockMode.Locked);
    }

    void Update()
    {
        if (!isDead)
        {
            MovementInput();

            MouseLook();

            FinalMovements();
        }
        else
        {
            Cursor.visible = true;
        }
    }

    public void LockCursor(CursorLockMode lockMode)
    {
        Cursor.lockState = lockMode;
        Cursor.visible = lockMode == CursorLockMode.Locked ? false : true;
    }

    #region Controllers
    private void MovementInput()
    {
        forward = transform.TransformDirection(Vector3.forward);
        right = transform.TransformDirection(Vector3.right);

        currentInput = new Vector2(walkingSpeed * Input.GetAxis("Vertical"), walkingSpeed * Input.GetAxis("Horizontal"));

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * currentInput.x) + (right * currentInput.y);
        moveDirection.y = movementDirectionY;
    }

    private void MouseLook()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void FinalMovements()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }            
        characterController.Move(moveDirection * Time.deltaTime);
    }
    #endregion

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DoorEnter") || other.CompareTag("DoorExit"))
        {
            Doors door = other.GetComponentInParent<Doors>();
            door.isCloseDoor = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DoorEnter"))
        {
            
            Doors door = other.GetComponentInParent<Doors>();
            
            if (door == null)
            {
                Debug.Log("AAAAAAAAAAAA");
            }
            else
            {
                
                    Debug.Log("A");
                
            }

            door.isCloseDoor = true;
            door.isEnter = true;

        }
        else if (other.CompareTag("DoorExit"))
        {
            Doors door = other.GetComponentInParent<Doors>();
            if (door == null)
            {
                Debug.Log("AAAAAAAAAAAA");
            }
            else
            {
                Debug.Log("A");
            }
            door.isCloseDoor = true;
            door.isEnter = false;
        }
        
    }
    
}