using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : Interactable, IKitchenObjectHolder {
  public override void Interact(IKitchenObjectHolder kitchenObjectHolder) {
    throw new System.NotImplementedException();
  }

  public Transform GetHoldingPoint() {
    throw new System.NotImplementedException();
  }

  public bool IsHoldingKitchenObject() {
    throw new System.NotImplementedException();
  }

  public void HoldKitchenObject(KitchenObject kitchenObject) {
    throw new System.NotImplementedException();
  }

  public void DropKitchenObject() {
    throw new System.NotImplementedException();
  }

  public KitchenObject GetKitchenObject() {
    throw new System.NotImplementedException();
  }
}