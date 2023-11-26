using UnityEngine;

public class ClearCounter : Interactable, IKitchenObjectHolder {
  [SerializeField] private KitchenObjectSO kitchenObjectData;
  [SerializeField] private Transform counterTopPoint;

  // Currently held kitchen object
  private KitchenObject _kitchenObject;

  public override void Interact(IKitchenObjectHolder kitchenObjectHolder) {
    if (IsHoldingKitchenObject() == false) {
      if (kitchenObjectHolder.IsHoldingKitchenObject()) {
        kitchenObjectHolder.GetKitchenObject().TransferTo(this);
      } else {
        
        var kitchenObjectInstance = Instantiate(this.kitchenObjectData.prefab);
        kitchenObjectInstance.GetComponent<KitchenObject>().TransferTo(this);
      }
    } else {
      _kitchenObject.TransferTo(kitchenObjectHolder);
    }
  }

  public Transform GetHoldingPoint() {
    return counterTopPoint;
  }

  public bool IsHoldingKitchenObject() {
    return _kitchenObject != null;
  }

  public void HoldKitchenObject(KitchenObject kitchenObject) {
    // Ignore if there is already a kitchen object
    if (IsHoldingKitchenObject()) return;

    // Store the kitchen object
    _kitchenObject = kitchenObject;
  }

  public void DropKitchenObject() {
    _kitchenObject = null;
  }

  public KitchenObject GetKitchenObject() {
    return _kitchenObject;
  }
}