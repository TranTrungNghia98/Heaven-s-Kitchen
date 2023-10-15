using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public GameObject gameObject;
        public KitchenObjectSO kitchenObjectSO;
    }

    public List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjectList;

    [SerializeField] PlateKitchenObject plateKitchenObject;

    private void Awake()
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjectList)
        {
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        plateKitchenObject.OnAddIngredient += PlateKitchenObject_OnAddIngredient1;
    }

    private void PlateKitchenObject_OnAddIngredient1(object sender, PlateKitchenObject.OnAddIngredientEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjectList)
        {
            if (kitchenObjectSO_GameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
    }

    
}
