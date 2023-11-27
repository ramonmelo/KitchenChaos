using System;
using UnityEngine;
using UnityEngine.Assertions;

public class KitchenObject : MonoBehaviour {
  [SerializeField] private KitchenObjectSO kitchenObject;

  public KitchenObjectSO GetKitchenObjectData() {
    return kitchenObject;
  }
  
  public IKitchenObjectHolder GetCurrentHolder() {
    return GetComponentInParent<IKitchenObjectHolder>();
  }
  
  public bool IsHeld() {
    return GetCurrentHolder() != null;
  }

  public void TransferTo(IKitchenObjectHolder newHolder) {
    Assert.IsNotNull(newHolder);
    
    // Ignore if the holder is already holding a kitchen object
    if (newHolder.IsHoldingKitchenObject()) return;
    
    // Drop any kitchen object that the holder is holding
    GetCurrentHolder()?.DropKitchenObject();
    
    // Parent the kitchen object to the holder's holding point
    var kitchenObjectTransform = transform;
    kitchenObjectTransform.parent = newHolder.GetHoldingPoint();
    kitchenObjectTransform.localPosition = Vector3.zero;
    
    // Store the kitchen object
    newHolder.HoldKitchenObject(this);
  }
}