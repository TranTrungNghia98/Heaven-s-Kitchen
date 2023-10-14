using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] List<KitchenObjectSO> kitchenObjectSOValidList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Start()
    {
        kitchenObjectSOList = new List<KitchenObjectSO> ();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        // If Ingredient valid or not
        if (!kitchenObjectSOValidList.Contains(kitchenObjectSO))
        {
            return false;
        }

        // CHeck Ingredient has putted on the plate or not
        if (kitchenObjectSOList.Contains (kitchenObjectSO))
        {
            return false;
        }

        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
        
    }
}
