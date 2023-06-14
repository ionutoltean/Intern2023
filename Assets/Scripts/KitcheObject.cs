using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitcheObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;
    [SerializeField] private IKitchenObjectParent _kitchenObjectParent;

    public KitchenObjectSO GetKitchenObject()
    {
        return _kitchenObjectSo;
    }

    public IKitchenObjectParent GetCurrentCounter()
    {
        return _kitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (_kitchenObjectParent != null)
            _kitchenObjectParent.SetKitchenObject(null);
        _kitchenObjectParent = kitchenObjectParent;
        transform.parent = kitchenObjectParent.GetCounterTopPoint();
        kitchenObjectParent.SetKitchenObject(this);
        transform.localPosition = Vector3.zero;
    }
}