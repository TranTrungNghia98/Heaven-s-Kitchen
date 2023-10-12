using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player)
    {
        // If nothing on the counter
        if (!HasKitchenObject())
        {
            // If player hold something
            if (player.HasKitchenObject())
            {
                // If player hold something can be cut. Put it on cutting counter
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
            // If player doesn't hold anything
            else
            {
                // DO nothing
            }
        }
        // Counter has something
        else
        {
            // If player hold something
            if (player.HasKitchenObject())
            {
                // Do nothing
            }
            // If player doesn't hold something
            else
            {
                // Player pick up kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void Interactnate(Player player)
    {
        // If cutting counter has something and it and it can be cut. Cut it
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()) )
        {
            KitchenObjectSO cuttingObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cuttingObjectSO, this);
        }

        else
        {

        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == kitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }

        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return true;
            }
        }

        return false;
    }

}
