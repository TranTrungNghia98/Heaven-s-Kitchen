using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }

        this.clearCounter = clearCounter;

        if (this.clearCounter.HasKitchenObject())
        {
            Debug.LogError("Can't put kitchen object here");
        }

        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowPoint();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
