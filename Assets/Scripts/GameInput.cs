using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {  get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAfternateAction;
    public event EventHandler OnPaused;

    private PlayerInputAction playerInputAction;
    private void Awake()
    {
        Instance = this;


        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Interact.performed += Interact_performed;
        playerInputAction.Player.InteractAfternate.performed += InteractAfternate_performed;
        playerInputAction.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInputAction.Player.Interact.performed -= Interact_performed;
        playerInputAction.Player.InteractAfternate.performed -= InteractAfternate_performed;
        playerInputAction.Player.Pause.performed -= Pause_performed;

        playerInputAction.Dispose();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPaused?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAfternate_performed(InputAction.CallbackContext obj)
    {
        OnInteractAfternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalize()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
