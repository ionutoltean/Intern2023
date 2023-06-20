using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitcheObject
{
    private List<KitchenObjectSO> _ingredientList;
    [SerializeField] private List<KitchenObjectSO> _validIngredientList;
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

   

    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSo;
    }

    private void Awake()
    {
        _ingredientList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO ingredient)
    {
        if (_validIngredientList.Contains(ingredient) == false)
            return false;
        if (_ingredientList.Contains(ingredient))
            return false;
        _ingredientList.Add(ingredient);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
        {
            KitchenObjectSo = ingredient
        });
        return true;
    }
}