using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerInputActions _playerInputActions;
    public EventHandler OnInteractPerformed;
    public EventHandler OnAlternateInteractPerformed;
    public EventHandler OnPausePerformed;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Interact.performed += InteractPerformed;
        _playerInputActions.Player.Pause.performed += PausePerformed;
        _playerInputActions.Player.AltInteract.performed += AltInteractPerformed;
        Instance = this;
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Interact.performed -= InteractPerformed;
        _playerInputActions.Player.Pause.performed -= PausePerformed;
        _playerInputActions.Player.AltInteract.performed -= AltInteractPerformed;
        _playerInputActions.Dispose();
    }

    private void PausePerformed(InputAction.CallbackContext obj)
    {
        OnPausePerformed?.Invoke(this, EventArgs.Empty);
    }

    private void AltInteractPerformed(InputAction.CallbackContext obj)
    {
        OnAlternateInteractPerformed?.Invoke(this, EventArgs.Empty);
        ;
    }

    private void InteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteractPerformed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputVector2Normalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}