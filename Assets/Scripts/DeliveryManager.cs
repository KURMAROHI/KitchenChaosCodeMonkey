using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSucess;
    public event EventHandler OnRecipeFail;
    public static DeliveryManager InStance { get; private set; }
    [SerializeField] private RecipeListSO _recipeSoList;
    private List<RecipeSO> _waitingRecipeSoList;

    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipeMax = 4;

    private void Awake()
    {
        if (InStance == null)
        {
            InStance = this;
        }
        _waitingRecipeSoList = new List<RecipeSO>();
    }

    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;
        if (_spawnRecipeTimer <= 0)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;
            if (_waitingRecipeSoList.Count < _waitingRecipeMax)
            {
                RecipeSO waitingRecipeSo = _recipeSoList.recipeListSO[UnityEngine.Random.Range(0, _recipeSoList.recipeListSO.Count)];
                Debug.Log("==>|" + waitingRecipeSo.RecipeName);
                _waitingRecipeSoList.Add(waitingRecipeSo);

                OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < _waitingRecipeSoList.Count; i++)
        {
            RecipeSO waitingRecipeSo = _waitingRecipeSoList[i];
            if (waitingRecipeSo.KitchenObjectsList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentMatchesRecipe = true;
                foreach (KitchenObjects recipeKitchenObjectSo in waitingRecipeSo.KitchenObjectsList)
                {
                    //checking all the ingredients in recipe
                    bool isIngredientFound = false;

                    foreach (KitchenObjects platekitchenObjectSo in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (platekitchenObjectSo == recipeKitchenObjectSo)
                        {
                            //ingredient Matches
                            isIngredientFound = true;
                            break;
                        }
                    }

                    if (!isIngredientFound)
                    {
                        //this recipe ingredient was not found on the plate
                        plateContentMatchesRecipe = false;
                    }
                }

                if (plateContentMatchesRecipe)
                {
                    //player Delivered Correct Recipe
                    Debug.Log("player Delivered Coreect recipe");
                    _waitingRecipeSoList.RemoveAt(i);
                    OnRecipeSucess?.Invoke(this,EventArgs.Empty);
                    OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                    return;
                }
            }

        }

        //no match found 
        //player Didnt Recive Coreect Recipe
        OnRecipeFail?.Invoke(this,EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSoList()
    {
        return _waitingRecipeSoList;
    }
}
