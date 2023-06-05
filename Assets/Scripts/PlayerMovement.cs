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
    private Vector2 currentInput;

    private float rotationX = 0;

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();

        LockCursor(CursorLockMode.Locked);
    }

    void Update()
    {
        MovementInput();

        MouseLook();

        FinalMovements();
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
}