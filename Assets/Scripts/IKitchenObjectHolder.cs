using UnityEngine;

public interface IKitchenObjectHolder {
  Transform GetHoldingPoint();
  bool IsHoldingKitchenObject();
  void HoldKitchenObject(KitchenObject kitchenObject);
  void DropKitchenObject();
  
  KitchenObject GetKitchenObject();
}