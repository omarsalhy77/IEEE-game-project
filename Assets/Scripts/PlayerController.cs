using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour 
{
    public static PlayerController Instance;
    [SerializeField] private float moveSpeed = 5f,rotationSpeed;
    [SerializeField] private Transform cameraTransform;

    private PlayerInputActions playerInputActions;
    private Vector2 inputVector;

    public event EventHandler OnInteractInput;

    private InputAction m_lookAction;


    public float sensitivity = 100f;
    
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        m_lookAction = InputSystem.actions.FindAction("Look");
        playerInputActions.Player.Interact.performed += OnInteractPerformed;
    }

    private void OnDestroy()
    {
        
        playerInputActions.Player.Interact.performed -= OnInteractPerformed;
    }

    private void Update()
    {
        HandleMovement();
        
    }

    private void HandleMovement()
    {
        inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Convert moveDir to be relative to camera forward and right vectors
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 relativeMoveDir = camForward * moveDir.z + camRight * moveDir.x;

        transform.position += relativeMoveDir.normalized * moveSpeed * Time.deltaTime;

        if (relativeMoveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(relativeMoveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        OnInteractInput?.Invoke(this, EventArgs.Empty);
        float distance = Vector3.Distance(transform.position, DoorOpenCLose.Instance.doorTransform.position);
        if (distance < 3)
        {
            if (DoorOpenCLose.Instance.doorIsOpen)
            {
                DoorOpenCLose.Instance.CloseDoor();
            }
            else 
            {
                DoorOpenCLose.Instance.OpenDoor();
            }
        }
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return inputVector.normalized;
    }
    



}
