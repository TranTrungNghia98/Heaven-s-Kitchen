using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        // If player is holding something
        if (player.HasKitchenObject())
        {
            // If PLayer is hlding the plate
            if (player.GetKitchenObject().TryGetPlateKitchenObject(out PlateKitchenObject plateKitchenObject))
            {
                // Destroy the plate
                player.GetKitchenObject().DestroySelf();
            }
        }

    }
}
