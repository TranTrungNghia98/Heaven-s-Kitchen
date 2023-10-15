using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler <OnAddIngredientEventArgs> OnAddIngredient;
    public class OnAddIngredientEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

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
            OnAddIngredient?.Invoke(this, new OnAddIngredientEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
        
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
