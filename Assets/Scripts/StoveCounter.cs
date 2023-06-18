using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
   [SerializeField]
   private FryingRecipeSO[] _fryingRecipeSOArray;

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
           if (player.HasKitchenObject() && HasIngredientForRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
           {
               KitcheObject kitcheObject = player.GetKitchenObject();
               kitcheObject.SetKitchenObjectParent(this);
              
           }
       }
   }
   
   private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
   {
       foreach (var recipe in _fryingRecipeSOArray)
       {
           if (recipe.input == input)
               return recipe.output;
       }

       return null;
   }

   private bool HasIngredientForRecipe(KitchenObjectSO input)
   {
       foreach (var recipe in _fryingRecipeSOArray)
       {
           if (recipe.input == input)
               return true;
       }

       return false;
   }

   private float GetSecondsNeededMaximum(KitchenObjectSO input)
   {
       foreach (var recipe in _fryingRecipeSOArray)
       {
           if (recipe.input == input)
               return recipe.fryingTimerMax;
       }

       Debug.LogError("Cuts NeededAreNot in the recipe array");
       return 0;
   }
}
