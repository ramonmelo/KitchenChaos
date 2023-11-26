using UnityEngine;

/// <summary>
/// Abstract class for interactable objects in the game.
/// Implements the IInteractable interface.
/// </summary>
public abstract class Interactable : MonoBehaviour, IInteractable {
  /// <summary>
  /// Visual representation of the selected counter.
  /// </summary>
  [SerializeField] private SelectedCounterVisual selectedCounterVisual;

  /// <summary>
  /// Subscribes to the OnInteractableChanged event at the start of the game.
  /// </summary>
  private void Start() {
    // Subscribe to the OnInteractableChanged event
    Player.Instance.OnInteractableChanged += InstanceOnOnInteractableChanged;
  }

  /// <summary>
  /// Handles the OnInteractableChanged event.
  /// Activates or deactivates the selectedCounterVisual based on the interactable object.
  /// </summary>
  /// <param name="interactable">The interactable object that triggered the event.</param>
  private void InstanceOnOnInteractableChanged(IInteractable interactable) {
    // If the interactable object is this instance, activate the visual
    if (interactable != null && interactable.Equals(this)) {
      selectedCounterVisual.Activate();
    } else {
      // Otherwise, deactivate the visual
      selectedCounterVisual.Deactivate();
    }
  }

  /// <summary>
  /// Abstract method for interaction with the object.
  /// Must be implemented in derived classes.
  /// </summary>
  public abstract void Interact(IKitchenObjectHolder kitchenObjectHolder);
}