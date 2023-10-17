using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnDroped;

    [SerializeField] Transform kitchenParentTopPoint;
    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.Log("Interact()");
    }

    public virtual void Interactnate(Player player)
    {
        Debug.Log("Interactnate()");
    }

    public Transform GetKitchenObjectFollowPoint()
    {
        return kitchenParentTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        OnDroped?.Invoke(this, EventArgs.Empty);
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
