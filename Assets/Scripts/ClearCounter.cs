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

   
}
