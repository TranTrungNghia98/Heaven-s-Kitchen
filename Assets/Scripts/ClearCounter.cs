using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] Transform counterTopPoint;

    [SerializeField] ClearCounter secondClearCounter;
    private KitchenObject kitchenObject;

    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }

        else
        {
            kitchenObject.SetClearCounter(secondClearCounter);
        }
    }

    public Transform GetKitchenObjectFollowPoint()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
