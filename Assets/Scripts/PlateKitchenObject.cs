using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjects> _validKitchenObjectSOList;
    private List<KitchenObjects> _kitchenObjectSOList;
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjects kitchenObjectSO;
    }


    private void Awake()
    {
        _kitchenObjectSOList = new List<KitchenObjects>();
    }
    public bool TryAddIngredient(KitchenObjects kitchenObjects)
    {
        if (!_validKitchenObjectSOList.Contains(kitchenObjects))
        {
            //not a valid ingredient
            return false;
        }

        if (_kitchenObjectSOList.Contains(kitchenObjects))
        {
            //Already has this type
            return false;
        }
        else
        {
            _kitchenObjectSOList.Add(kitchenObjects);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjects
            });
            return true;
        }
    }

    public List<KitchenObjects> GetKitchenObjectSOList()
    {
        return _kitchenObjectSOList;
    }
}
