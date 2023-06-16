using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    public EventHandler OnInteractPerformed;
    public EventHandler OnAlternateInteractPerformed;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Interact.performed += InteractPerformed;
        _playerInputActions.Player.AltInteract.performed += AltInteractPerformed;
    }

    private void AltInteractPerformed(InputAction.CallbackContext obj)
    {
        OnAlternateInteractPerformed?.Invoke(this, EventArgs.Empty);;
    }

    private void InteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteractPerformed?.Invoke(this, EventArgs.Empty);;
    }

    public Vector2 GetInputVector2Normalized()
    {

        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
  
}
