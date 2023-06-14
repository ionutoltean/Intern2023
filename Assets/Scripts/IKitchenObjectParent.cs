using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetCounterTopPoint();

    public KitcheObject GetKitchenObject();

    public void SetKitchenObject(KitcheObject kitcheObject);

    public bool HasKitchenObject();
    public void ClearKitchenObject();
}
