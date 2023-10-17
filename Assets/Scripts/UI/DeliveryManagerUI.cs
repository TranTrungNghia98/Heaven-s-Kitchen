using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnSpawnRecipe += DeliveryManager_OnSpawnRecipe;
        DeliveryManager.Instance.OnCompleteRecipe += DeliveryManager_OnCompleteRecipe;

        UpdateVisual();
    }

    private void DeliveryManager_OnCompleteRecipe(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnSpawnRecipe(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        List <RecipeObjectSO> waitingRecipeObjectList = DeliveryManager.Instance.GetWaitingRecipeObject();

        foreach(RecipeObjectSO waitingRecipeObject in waitingRecipeObjectList)
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeObjectSO(waitingRecipeObject);
        }
    }
}
