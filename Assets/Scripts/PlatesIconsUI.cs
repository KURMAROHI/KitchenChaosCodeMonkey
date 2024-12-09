using System;
using UnityEngine;

public class PlatesIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private Transform _iconTemplate;

    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();

    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == _iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjects kitchenObjectSO in _plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransFrom = Instantiate(_iconTemplate, transform);
            _iconTemplate.gameObject.SetActive(true);
            iconTransFrom.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
