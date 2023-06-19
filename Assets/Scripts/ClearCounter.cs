using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
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
            else
            {
                if (player.GetKitchenObject().TryToGetPlate(out PlateKitchenObject plate))
                {
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        GetKitchenObject().DestroySelf();
                }

                else if (GetKitchenObject().TryToGetPlate(out PlateKitchenObject plate2))
                {
                    if (plate2.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        player.GetKitchenObject().DestroySelf();
                }
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