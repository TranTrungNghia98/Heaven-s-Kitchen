using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] KitchenObjectSO kitchenObjectSO;
    

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4;
    private int plateSpawnAmount;
    private int plateSpawnAmountMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= spawnPlateTimerMax)
        {
            spawnPlateTimer = 0;
            plateSpawnAmount++;
            
            if (plateSpawnAmount <= plateSpawnAmountMax)
            {
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        // Player doesn't hold anything
        if (!player.HasKitchenObject())
        {
            // Plate spawned > 0. So player can pick up plate
            if (plateSpawnAmount > 0)
            {
                plateSpawnAmount--;
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
