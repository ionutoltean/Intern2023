using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour , IKitchenObjectParent
{

    public static event EventHandler OnDrop;

    public static void ResetData()
    {
        OnDrop = null;
    }
    [SerializeField] private GameObject _counterTopPoint;
    private KitcheObject _kitchenObject;
    // Start is called before the first frame update
    public virtual void Interact(Player player)
    {
        
    }
    
    public Transform GetCounterTopPoint()
    {
        return _counterTopPoint.transform;
    }

    public KitcheObject GetKitchenObject()
    {
        return _kitchenObject;
      
    }

    public void SetKitchenObject(KitcheObject kitcheObject)
    {
        this._kitchenObject = kitcheObject;
        if(kitcheObject!=null)
            OnDrop?.Invoke(this,EventArgs.Empty);
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public virtual void InteractAlternate(Player player)
    {
        
    }
}
