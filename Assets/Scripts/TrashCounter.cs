using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTrashed;

    public static void ResetData()
    {
        OnTrashed = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            KitcheObject kitcheObject = player.GetKitchenObject();
            kitcheObject.SetKitchenObjectParent(this);
            GetKitchenObject().DestroySelf();
            OnTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}