/// <summary>
/// Defines the interface for interactable objects in the game.
/// </summary>
public interface IInteractable {
  /// <summary>
  /// Method to be implemented by any class that implements this interface.
  /// This method will define the interaction behavior of the object.
  /// </summary>
  void Interact(IKitchenObjectHolder kitchenObjectHolder);
}