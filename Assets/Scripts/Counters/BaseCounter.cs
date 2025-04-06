using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    private KitchenObject _kitchenObject;
    [SerializeField] private Transform _counterTopPoint;

    public static event EventHandler OnAnyObjectPlacedHere;


    public virtual void Interact(Player player)
    {
        Debug.Log("Base Counter");
    }
    public virtual void InteractAltranate(Player player)
    {
        Debug.Log("Base Counter InteractAltranate");
    }


    public Transform GetKitchenObjectFollowTransFrom()
    {
        return _counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;
        if(kitchenObject!=null)
        {
            OnAnyObjectPlacedHere?.Invoke(this,EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
