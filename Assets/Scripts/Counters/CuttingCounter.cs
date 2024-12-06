using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSoArray;
    private int _cuttinProgress;

    public event EventHandler<IHasProgress.OnProgreessChangedEventArgs> OnProgress_Changed;

    public event EventHandler On_Cut;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //No kitchen Object
            if (player.HasKitchenObject())
            {
                //player is Carrying Something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carrying somthing that Shoud be Cut 
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    _cuttinProgress = 0;
                    // CuttingRecipeSO cuttingRecipeSO = GettingCuttinRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());


                }
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
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            _cuttinProgress++;

            On_Cut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GettingCuttinRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgress_Changed?.Invoke(this, new IHasProgress.OnProgreessChangedEventArgs
            {
                ProgreessNormalized = (float)_cuttinProgress / cuttingRecipeSO.CuttingProgressMax
            });

            if (_cuttinProgress >= cuttingRecipeSO.CuttingProgressMax)
            {
                KitchenObjects outputKitchrnObject = GetOutPutForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchrnObject, this);
            }
        }
    }

    private KitchenObjects GetOutPutForInput(KitchenObjects inputKitchenObjectSO)
    {
        CuttingRecipeSO currentRecipeSo = GettingCuttinRecipeWithInput(inputKitchenObjectSO);
        return currentRecipeSo != null ? currentRecipeSo.OutPut : null;

    }

    private bool HasRecipeWithInput(KitchenObjects inputKitchenObjectSO)
    {
        CuttingRecipeSO currentRecipeSo = GettingCuttinRecipeWithInput(inputKitchenObjectSO);
        return currentRecipeSo != null;
    }

    private CuttingRecipeSO GettingCuttinRecipeWithInput(KitchenObjects inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO currentRecipeSo in _cuttingRecipeSoArray)
        {
            if (currentRecipeSo.Input == inputKitchenObjectSO)
            {
                return currentRecipeSo;
            }
        }

        return null;

    }
}
