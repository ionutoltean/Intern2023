using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            //object on the counter 
            if (player.HasKitchenObject() == false)
            {
                KitcheObject kitcheObject = GetKitchenObject();
                kitcheObject.SetKitchenObjectParent(player);
                
            }
        }
        else
        {
            //no object
            if (player.HasKitchenObject())
            {
                KitcheObject kitcheObject = player.GetKitchenObject();
                kitcheObject.SetKitchenObjectParent(this);
            }
            
        }

    }
}
