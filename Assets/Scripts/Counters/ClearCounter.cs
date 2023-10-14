using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    public override void Interact(Player player)
    {
        // If nothing on the counter
        if (!HasKitchenObject())
        {
            // If player hold something
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
            // If player is holding something
            if (player.HasKitchenObject())
            {
                // If player is holding a plate
                if (player.GetKitchenObject().TryGetPlateKitchenObject(out PlateKitchenObject plateKitchenObject))
                {
                    // Try to add ingredient from the counter to the plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }     
                }

                // If player isn't holding a plate
                else
                {
                    // If has a plate on the clear counter
                    if (GetKitchenObject().TryGetPlateKitchenObject(out plateKitchenObject))
                    {
                        // Try to add ingredient from the player to the plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            // If player doesn't hold something
            else
            {
                // Player pick up kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}
