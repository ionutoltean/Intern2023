using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public override void Interact(Player player)
    {

            if (player.HasKitchenObject())
            {
                KitcheObject kitcheObject = player.GetKitchenObject();
                kitcheObject.SetKitchenObjectParent(this);
                GetKitchenObject().DestroySelf();
            }
            
       
        
    }

}