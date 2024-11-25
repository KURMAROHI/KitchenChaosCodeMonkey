using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjects _cutKitchenObject;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //No kitchen Object
            if (player.HasKitchenObject())
            {
                //player is Carrying Something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //
            }
        }
        else
        {
            //has kitchen Object
            if (player.HasKitchenObject())
            {
                //player has kitchen object so player cant carry 2 objects
            }
            else
            {
                //player not Carrying Anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


    public override void InteractAltranate(Player player)
    {
        if (HasKitchenObject())
        {
            GetKitchenObject().DestroySelf();

            Transform kitchenObjectTransfrom = Instantiate(_cutKitchenObject.Prefab);
            kitchenObjectTransfrom.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
    }
}
