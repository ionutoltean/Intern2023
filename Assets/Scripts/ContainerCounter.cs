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


        KitcheObject.SpawnKitchenObject(_kitchenObjectSO, player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}