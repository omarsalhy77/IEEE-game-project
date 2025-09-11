using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour 
{
    public static PlayerController Instance;
    [SerializeField] private float moveSpeed = 5f,rotationSpeed;
   
    private PlayerInputActions playerInputActions;
    private Vector2 inputVector;

    public event EventHandler OnInteractInput;



    public float sensitivity = 100f;
    //private float rotationX = 0f;
    //private float rotationY = 0f;
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += OnInteractPerformed;
    }

    private void OnDestroy()
    {
        //Debug.Log(player.transform.position);
        playerInputActions.Player.Interact.performed -= OnInteractPerformed;
    }

    private void Update()
    {
        HandleMovement();
        //Camera();
    }

    private void HandleMovement()
    {
        //transform.rotation = transform.rotation*rotationSpeed * Time.deltaTime;
        inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        OnInteractInput?.Invoke(this, EventArgs.Empty);
        DoorOpenCLose.Instance.DoorOpen();
        if (!DoorOpenCLose.Instance.doorIsOpen)
        {
            DoorOpenCLose.Instance.CloseDoor();
        }
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return inputVector.normalized;
    }

   

}
