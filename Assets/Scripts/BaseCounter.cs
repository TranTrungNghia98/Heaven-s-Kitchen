using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] Transform kitchenParentTopPoint;

    [SerializeField] BaseCounter secondClearCounter;
    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.Log("Interact()");
    }

    public Transform GetKitchenObjectFollowPoint()
    {
        return kitchenParentTopPoint;
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
