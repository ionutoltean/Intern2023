using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;


    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += RecipeCompleted;
        DeliveryManager.Instance.OnRecipeAdded+= RecipeAdded;
        UpdateVisual();
    }

    private void RecipeAdded(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void RecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if(child == recipeTemplate)continue;
            Destroy(child.gameObject);
        }

        foreach (var recipeSo in DeliveryManager.Instance.GetWaitingOrders())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSo(recipeSo);
        }
    }
}
