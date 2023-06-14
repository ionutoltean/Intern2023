using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    [SerializeField] private GameObject _counterTopPoint;
    [SerializeField] private KitcheObject _kitcheObject;
    public bool testing;
    public ClearCounter secondCounter;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (secondCounter.GetKitchenObject() == null)
                if (_kitcheObject != null)
                    _kitcheObject.SetKitchenObjectParent(secondCounter);
        }
    }

    public void Interact(Player player)
    {
        if (_kitcheObject == null)
        {
            GameObject spawnedItem = Instantiate(_kitchenObjectSO.prefab, _counterTopPoint.transform);
            spawnedItem.transform.localPosition = Vector3.zero;
            _kitcheObject = spawnedItem.GetComponent<KitcheObject>();
            _kitcheObject.SetKitchenObjectParent(this);
        }
        else
        {
            _kitcheObject.SetKitchenObjectParent(player);
        }
    }

    public Transform GetCounterTopPoint()
    {
        return _counterTopPoint.transform;
    }

    public KitcheObject GetKitchenObject()
    {
        return _kitcheObject;
    }

    public void SetKitchenObject(KitcheObject kitcheObject)
    {
        this._kitcheObject = kitcheObject;
    }

    public bool HasKitchenObject()
    {
        return _kitcheObject != null;
    }

    public void ClearKitchenObject()
    {
        _kitcheObject = null;
    }
}