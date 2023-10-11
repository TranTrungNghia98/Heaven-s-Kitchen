using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent iKitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenParent)
    {
        if (this.iKitchenObjectParent != null)
        {
            this.iKitchenObjectParent.ClearKitchenObject();
        }

        this.iKitchenObjectParent = kitchenParent;

        if (this.iKitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Can't put kitchen object here");
        }

        kitchenParent.SetKitchenObject(this);

        transform.parent = kitchenParent.GetKitchenObjectFollowPoint();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return iKitchenObjectParent;
    }
}
