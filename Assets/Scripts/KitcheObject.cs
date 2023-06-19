using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitcheObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;
    [SerializeField] private IKitchenObjectParent _kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return _kitchenObjectSo;
    }

    public IKitchenObjectParent GetCurrentCounter()
    {
        return _kitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        // also sets the refferences
        if (_kitchenObjectParent != null)
            _kitchenObjectParent.SetKitchenObject(null);

        _kitchenObjectParent = kitchenObjectParent;
        transform.parent = kitchenObjectParent.GetCounterTopPoint();
        kitchenObjectParent.SetKitchenObject(this);
        transform.localPosition = Vector3.zero;
    }

    public void DestroySelf()
    {
        _kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryToGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
    public static KitcheObject SpawnKitchenObject(KitchenObjectSO objectSo, IKitchenObjectParent kitchenObjectParent)
    {
        GameObject spawnedItem = Instantiate(objectSo.prefab);
       KitcheObject kitcheObject =  spawnedItem.transform.GetComponent<KitcheObject>();
           kitcheObject.SetKitchenObjectParent(kitchenObjectParent);
           return kitcheObject;
    }
}