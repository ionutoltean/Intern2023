using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
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
            if (_kitcheObject != null)
                _kitcheObject.SetCurrentCounter(secondCounter);
        }
    }

    public void Interact()
    {
        if (_kitcheObject == null)
        {
            GameObject spawnedItem = Instantiate(_kitchenObjectSO.prefab, _counterTopPoint.transform);
            spawnedItem.transform.localPosition = Vector3.zero;
            _kitcheObject = spawnedItem.GetComponent<KitcheObject>();
            _kitcheObject.SetCurrentCounter(this);

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
}