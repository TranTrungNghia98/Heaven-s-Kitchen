using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
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

    public void DestroySelf()
    {
        GetKitchenObjectParent().ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlateKitchenObject(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }

        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab);

        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
}
