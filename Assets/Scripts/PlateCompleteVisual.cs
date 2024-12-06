using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private List<KitchenObjectSo_GameObject> _KitchenObjectSoGameObjectList;

    [Serializable]
    public struct KitchenObjectSo_GameObject
    {
        public KitchenObjects KitchenObjectSO;
        public GameObject gameObject;
    }
    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (KitchenObjectSo_GameObject KitchenObjectSoGameObject in _KitchenObjectSoGameObjectList)
        {
            KitchenObjectSoGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSo_GameObject KitchenObjectSoGameObject in _KitchenObjectSoGameObjectList)
        {
            if (e.kitchenObjectSO == KitchenObjectSoGameObject.KitchenObjectSO)
            {
                KitchenObjectSoGameObject.gameObject.SetActive(true);
            }
        }
    }
}
