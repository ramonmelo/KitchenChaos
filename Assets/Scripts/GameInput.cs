using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

  public event EventHandler OnInteractAction;

  private PlayerInputActions _playerActions;

  private void Awake() {
    _playerActions = new PlayerInputActions();
    _playerActions.Player.Enable();

    _playerActions.Player.Interact.performed += Interact_performed;
  }

  private void Interact_performed(InputAction.CallbackContext context) {
    OnInteractAction?.Invoke(this, EventArgs.Empty);
  }

  public Vector2 GetMovementVectorNormalized() => _playerActions.Player.Move.ReadValue<Vector2>();
}
