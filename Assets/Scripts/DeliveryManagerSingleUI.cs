using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplete;

    private void Awake()
    {
        iconTemplete.gameObject.SetActive(false);
    }

    public void SetRecipeSo(RecipeSO recipeSo)
    {
        recipeNameText.text = recipeSo.name;
        foreach (Transform child in iconContainer)
        {
            if(child==iconTemplete)continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSo in recipeSo.KitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplete, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSo.sprite;
        }
    }
}
