using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ClearCounter : BaseCounter
{

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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding plate 
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //player is not carrying plate we will check plate in on table then we can drop valid items on plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                //player not Carrying Anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}
