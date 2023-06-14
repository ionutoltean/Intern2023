using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter,IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    [SerializeField] private GameObject _counterTopPoint;
    [SerializeField] private KitcheObject _kitchenObject;
    public void Interact(Player player)
    {
            if(player.HasKitchenObject())return;
        if (_kitchenObject == null)
        {
            GameObject spawnedItem = Instantiate(_kitchenObjectSO.prefab, _counterTopPoint.transform);
            spawnedItem.transform.localPosition = Vector3.zero;
            _kitchenObject = spawnedItem.GetComponent<KitcheObject>();
            _kitchenObject.SetKitchenObjectParent(player);
        }
       
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
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }
}
