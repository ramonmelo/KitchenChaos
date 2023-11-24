using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls all Inputs for the Game
/// </summary>
public class GameInput : MonoBehaviour {

  /// <summary>
  /// Event that is invoked when the Interact button is pressed
  /// </summary>
  public event Action OnInteractAction;

  /// <summary>
  /// The PlayerInputActions asset
  /// </summary>
  private PlayerInputActions _playerActions;

  private void Awake() {
    _playerActions = new PlayerInputActions();
    _playerActions.Player.Enable();

    // Register the Interact Action
    _playerActions.Player.Interact.performed += Interact_performed;
  }

  /// <summary>
  /// Invokes the OnInteractAction event
  /// </summary>
  /// <param name="context"></param>
  private void Interact_performed(InputAction.CallbackContext context) {
    OnInteractAction?.Invoke();
  }

  /// <summary>
  /// Returns the Movement Vector normalized
  /// </summary>
  public Vector2 GetMovementVectorNormalized() => _playerActions.Player.Move.ReadValue<Vector2>();
}
