using UnityEngine;
using UnityEngine.Rendering;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjects _kitchenObjects;
    private IKitchenObjectParent _kitchenObjectParent;

    public KitchenObjects GetKitchenObject()
    {
        return _kitchenObjects;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this._kitchenObjectParent != null)
        {
            this._kitchenObjectParent.ClearKitchenObject();
        }

        this._kitchenObjectParent = kitchenObjectParent;
        if(this._kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("already Counter Contains Kitchen Object");
        }
        this._kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransFrom();
        transform.localPosition = Vector3.zero;

    }

    public IKitchenObjectParent GetClearCounter()
    {
        return _kitchenObjectParent;
    }

    public void DestroySelf()
    {
        _kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }


}