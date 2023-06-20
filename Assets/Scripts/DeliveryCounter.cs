using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {

        if (player.HasKitchenObject() && player.GetKitchenObject().TryToGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            KitcheObject kitcheObject = player.GetKitchenObject();
            kitcheObject.SetKitchenObjectParent(this);
            GetKitchenObject().DestroySelf();
        }
            
       
        
    }
}
