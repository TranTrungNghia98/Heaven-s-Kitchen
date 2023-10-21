using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnSpawnRecipe;
    public event EventHandler OnCompleteRecipe;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFailure;

    [SerializeField] RecipeObjectSOListSO recipeList;

    private List<RecipeObjectSO> waitingRecipeList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4;
    private int waittingRecipeMax = 3;
    private int successRecipeDelivered;

    private void Awake()
    {
        Instance = this;
        waitingRecipeList = new List<RecipeObjectSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        // Spawn new recipe after few seconds, recipe not max and game are playing
        if (GameManager.Instance.arePlaying() && spawnRecipeTimer <= 0 && waitingRecipeList.Count <= waittingRecipeMax)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            RecipeObjectSO recipeObjectSO = recipeList.recipeObjectSOList[UnityEngine.Random.Range(0, recipeList.recipeObjectSOList.Count)];
            waitingRecipeList.Add(recipeObjectSO);

            OnSpawnRecipe?.Invoke(this, EventArgs.Empty);
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject) 
    { 
        for (int i = 0; i < waitingRecipeList.Count; i++)
        {
            RecipeObjectSO recipeObjectSO = waitingRecipeList[i];

            // Check number ingredients in recipe with ingredients in plate kitchen object
            // If number of ingredients are the same. Continue compare
            if (recipeObjectSO.ingredientList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // Assume that plate's ingredient match with recipe's ingredient
                bool plateContentsMatchRecipe = true;
                // Check the match between plate's ingredient match with recipe's ingredient
                foreach (KitchenObjectSO recipeKitchenObjectSO in recipeObjectSO.ingredientList)
                {
                    bool recipeFound = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            recipeFound = true;
                            break;
                        }
                    }

                    // If can't find recipe from plate in recipe
                    if (!recipeFound)
                    {
                        // The Assume that plate's ingredient match with recipe's ingredient wrong
                        plateContentsMatchRecipe = false;
                    }



                }

                // After check. If Assume that plate's ingredient match with recipe's ingredient still true
                // Plate's ingredient match with recipe's ingredient
                if (plateContentsMatchRecipe)
                {
                    waitingRecipeList.Remove(recipeObjectSO);
                    successRecipeDelivered++;

                    OnCompleteRecipe?.Invoke(this, EventArgs.Empty);
                    // Active OnDeliverySuccess event to play sound effects
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        // Active OnDeliveryFailure event to play sound effects
        OnDeliveryFailure?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeObjectSO> GetWaitingRecipeObject()
    {
        return waitingRecipeList;
    }

    public int GetSuccessRecipeDelivered()
    {
        return successRecipeDelivered;
    }
}
