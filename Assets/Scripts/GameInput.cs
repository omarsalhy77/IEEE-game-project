using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
  

    private PlayerInputActions playerInputAction;

    public event EventHandler OnInteractInput;

    private void Awake()
    {
        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.Enable();

        playerInputAction.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractInput?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        
       /* if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1f;
        }*/


        return inputVector;
    }
}
