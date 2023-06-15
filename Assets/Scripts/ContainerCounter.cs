using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] protected KitchenObjectSO _kitchenObjectSO;
    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) return;

        GameObject spawnedItem = Instantiate(_kitchenObjectSO.prefab, GetCounterTopPoint().transform);
        SetKitchenObject(spawnedItem.GetComponent<KitcheObject>());
        GetKitchenObject().SetKitchenObjectParent(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}