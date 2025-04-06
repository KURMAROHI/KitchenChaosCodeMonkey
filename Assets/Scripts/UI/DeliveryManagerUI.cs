using System;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _recipeTemplate;

    private void Awake()
    {
        _recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
       DeliveryManager.InStance.OnRecipeSpawned+=DeliveryManagerUI_OnRecipeSpawned; 
       DeliveryManager.InStance.OnRecipeCompleted+=DeliveryManagerUI_OnRecipeCompleted; 

       UpdateVisual();
    }

    private void DeliveryManagerUI_OnRecipeCompleted(object sender, EventArgs e)
    {
       UpdateVisual();
    }

    private void DeliveryManagerUI_OnRecipeSpawned(object sender, EventArgs e)
    {
       UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in _container)
        {
            if(child== _recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(RecipeSO recipeSO in DeliveryManager.InStance.GetWaitingRecipeSoList())
        {
            Transform recipeTransform = Instantiate(_recipeTemplate,_container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSo(recipeSO);
        }
    }

}
