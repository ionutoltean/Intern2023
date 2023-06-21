using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private RecipeListSO _recipeListSo;
    public event EventHandler OnRecipeAdded; 
    public event EventHandler OnRecipeCompleted; 
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer = 0f;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    public static DeliveryManager Instance { get; private set; }

    private void Awake()
    {
        waitingRecipeSOList = new List<RecipeSO>();
        Instance = this;
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0 && waitingRecipeSOList.Count < waitingRecipesMax)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            RecipeSO nextRecipe =
                _recipeListSo.recipeSOList[UnityEngine.Random.Range(0, _recipeListSo.recipeSOList.Count)];
            waitingRecipeSOList.Add(nextRecipe);
            OnRecipeAdded?.Invoke(this,EventArgs.Empty);
        }
    }

    public List<RecipeSO> GetWaitingOrders()
    {
        return waitingRecipeSOList;
    }
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            bool recipeOk = false;
            if (plateKitchenObject.GetIngredientList().Count == waitingRecipeSOList[i].KitchenObjectSOList.Count)
            {
                recipeOk = true;
                /// same number of ingredients
                foreach (var ingredientRecipe in plateKitchenObject.GetIngredientList())
                {
                    if (waitingRecipeSOList[i].KitchenObjectSOList.Contains(ingredientRecipe) == false)
                        recipeOk = false;
                }
            }

            if (recipeOk)
            {
                waitingRecipeSOList.RemoveAt(i);
                OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                return;
            }
        }
    }
}