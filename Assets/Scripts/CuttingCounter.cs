using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipesArray;
    private int cutCount;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            //object on the counter 
            if (player.HasKitchenObject() == false)
            {
                KitcheObject kitcheObject = GetKitchenObject();
                kitcheObject.SetKitchenObjectParent(player);
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
            else
            {
                if (player.GetKitchenObject().TryToGetPlate(out PlateKitchenObject plate))
                {
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                
            }
        }
        else
        {
            //no object
            if (player.HasKitchenObject() && HasIngredientForRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                KitcheObject kitcheObject = player.GetKitchenObject();
                kitcheObject.SetKitchenObjectParent(this);
                cutCount = 0;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = (float)cutCount / GetCutsNeededMaximum(kitcheObject.GetKitchenObjectSO())
                });
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        KitchenObjectSO currentKitchenObject = GetKitchenObject().GetKitchenObjectSO();
        if (HasKitchenObject() && HasIngredientForRecipe(currentKitchenObject))
        {
            cutCount++;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cutCount / GetCutsNeededMaximum(GetKitchenObject().GetKitchenObjectSO())
            });
            if (player.HasKitchenObject() == false && cutCount >= GetCutsNeededMaximum(currentKitchenObject))
            {
                KitchenObjectSO kitchenObjectSo = GetOutputForInput(currentKitchenObject);
                GetKitchenObject().DestroySelf();
                KitcheObject.SpawnKitchenObject(kitchenObjectSo, this);
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        foreach (var recipe in cuttingRecipesArray)
        {
            if (recipe.input == input)
                return recipe.output;
        }

        return null;
    }

    private bool HasIngredientForRecipe(KitchenObjectSO input)
    {
        foreach (var recipe in cuttingRecipesArray)
        {
            if (recipe.input == input)
                return true;
        }

        return false;
    }

    private int GetCutsNeededMaximum(KitchenObjectSO input)
    {
        foreach (var recipe in cuttingRecipesArray)
        {
            if (recipe.input == input)
                return recipe.maximumNeededCuts;
        }

        Debug.LogError("Cuts NeededAreNot in the recipe array");
        return 0;
    }
}