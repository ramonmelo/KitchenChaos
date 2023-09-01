using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable {
  [SerializeField] private SelectedCounterVisual selectedCounterVisual;

  private void Start() {
    Player.Instance.OnInteractableChanged += InstanceOnOnInteractableChanged;
  }

  private void InstanceOnOnInteractableChanged(IInteractable inter) {
    if (inter != null && inter.Equals(this)) {
      selectedCounterVisual.Activate();
    } else {
      selectedCounterVisual.Deactivate();
    }
  }

  public abstract void Interact();
}