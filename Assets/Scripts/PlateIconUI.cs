using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] Transform iconTemplate;

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnAddIngredient += PlateKitchenObject_OnAddIngredient;
    }

    private void PlateKitchenObject_OnAddIngredient(object sender, PlateKitchenObject.OnAddIngredientEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        kitchenObjectSOList = plateKitchenObject.GetKitchenObjectSOList();

        foreach(KitchenObjectSO kitchenObjectSO in kitchenObjectSOList)
        {
            Transform newIconTemplate = Instantiate(iconTemplate, transform);
            newIconTemplate.gameObject.SetActive(true);
            newIconTemplate.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
