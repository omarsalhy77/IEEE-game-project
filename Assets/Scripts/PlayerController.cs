using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour 
{
    [SerializeField] private float moveSpeed = 5f;

    private PlayerInputActions playerInputActions;
    private Vector2 inputVector;

    public event EventHandler OnInteractInput;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

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
        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        OnInteractInput?.Invoke(this, EventArgs.Empty);
        Debug.Log("Interact button pressed");
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return inputVector.normalized;
    }

}
