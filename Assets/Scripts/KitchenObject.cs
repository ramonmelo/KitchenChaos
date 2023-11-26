using System;
using UnityEngine;
using UnityEngine.Assertions;

public class KitchenObject : MonoBehaviour {
  [SerializeField] private KitchenObjectSO kitchenObject;

  public KitchenObjectSO GetKitchenObjectData() {
    return kitchenObject;
  }
  
  public IKitchenObjectHolder GetHolder() {
    return GetComponentInParent<IKitchenObjectHolder>();
  }
  
  public bool IsHeld() {
    return GetHolder() != null;
  }

  public void TransferTo(IKitchenObjectHolder holder) {
    Assert.IsNotNull(holder);
    
    // Ignore if the holder is already holding a kitchen object
    if (holder.IsHoldingKitchenObject()) return;
    
    // Drop any kitchen object that the holder is holding
    GetHolder()?.DropKitchenObject();
    
    // Parent the kitchen object to the holder's holding point
    var kitchenObjectTransform = transform;
    kitchenObjectTransform.parent = holder.GetHoldingPoint();
    kitchenObjectTransform.localPosition = Vector3.zero;
    
    // Store the kitchen object
    holder.HoldKitchenObject(this);
  }
}