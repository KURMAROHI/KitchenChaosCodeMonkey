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
            KitchenObject.SpawnKitchenObject(_kitchenObjectss, player);
            OnPlayerGranbObject?.Invoke(this, EventArgs.Empty);
        }
    }




}
