using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {

        if (player.HasKitchenObject() && player.GetKitchenObject().TryToGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            KitcheObject kitcheObject = player.GetKitchenObject();
            kitcheObject.SetKitchenObjectParent(this);
            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
            GetKitchenObject().DestroySelf();
        }
            
       
        
    }
}
