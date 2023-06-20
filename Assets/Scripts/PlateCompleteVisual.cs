using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField] private GameObject plateCompleteVisual;
    [SerializeField] private PlateKitchenObject plateCKitchenObject;

    [Serializable]
    public struct KitchenObjectSo_GameObject
    {
        public KitchenObjectSO KitchenObjectSo;
        public GameObject KitchenObjectGameObject;
    }

    public List<KitchenObjectSo_GameObject> KitchenObjectSoGameObjectsList;

    private void Start()
    {
        plateCKitchenObject.OnIngredientAdded += OnIngredientAdded;
    }

    private void OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (var burgerObject in KitchenObjectSoGameObjectsList)
        {
            if (e.KitchenObjectSo == burgerObject.KitchenObjectSo)
                burgerObject.KitchenObjectGameObject.SetActive(true);
        }
    }
}