using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

  public event EventHandler OnInteractAction;

  private PlayerInputActions playerActions;

  private void Awake() {
    playerActions = new PlayerInputActions();
    playerActions.Player.Enable();

    playerActions.Player.Interact.performed += Interact_performed;
  }

  private void Interact_performed(InputAction.CallbackContext context) {
    OnInteractAction?.Invoke(this, EventArgs.Empty);
  }

  public Vector2 GetMovementVectorNormalized() => playerActions.Player.Move.ReadValue<Vector2>();
}
