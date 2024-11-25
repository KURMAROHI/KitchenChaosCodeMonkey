using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjects _kitchenObjectss;
    public event EventHandler OnPlayerGranbObject;


    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player is carrying something
            Transform kitchenObjectTransfrom = Instantiate(_kitchenObjectss.Prefab);
            kitchenObjectTransfrom.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGranbObject?.Invoke(this, EventArgs.Empty);
        }
    }




}
