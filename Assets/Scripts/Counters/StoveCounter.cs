using System;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] _fryingRecipeSoArray;
    [SerializeField] private BurningRecipeSO[] _burningRecipeSoArray;
    private float _fryingTimer;
    private float _burningTimer;
    private FryingRecipeSO _fryingRecipeSo;
    private BurningRecipeSO _burningRecipeSo;
    private State _state;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    private void Start()
    {
        _state = State.Idle;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.Idle:
                break;
            case State.Frying:
                if (HasKitchenObject())
                {
                    _fryingTimer += Time.deltaTime;
                    if (_fryingTimer >= _fryingRecipeSo.FryingTimeMax)
                    {
                        _fryingTimer = 0;
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(_fryingRecipeSo.OutPut, this);
                        _state = State.Fried;
                        _burningTimer = 0;
                        _burningRecipeSo = GettingBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = _state
                        });
                    }
                }
                break;
            case State.Fried:
                if (HasKitchenObject())
                {
                    _burningTimer += Time.deltaTime;
                    if (_burningTimer >= _burningRecipeSo.BurningTimeMax)
                    {
                        _burningTimer = 0;
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(_burningRecipeSo.OutPut, this);
                        _state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = _state
                        });
                    }
                }
                break;
            case State.Burned:
                break;
        }

    }

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
                    //player carrying somthing that Shoud be Fry 
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    _fryingRecipeSo = GettingFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    _state = State.Frying;
                    _fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = _state
                    });
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
            }
            else
            {
                //player not Carrying Anything
                GetKitchenObject().SetKitchenObjectParent(player);
                _state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = _state
                });

            }
        }
    }



    private KitchenObjects GetOutPutForInput(KitchenObjects inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSo = GettingFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSo != null ? fryingRecipeSo.OutPut : null;

    }

    private bool HasRecipeWithInput(KitchenObjects inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSo = GettingFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSo != null;
    }

    private FryingRecipeSO GettingFryingRecipeSOWithInput(KitchenObjects inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSo in _fryingRecipeSoArray)
        {
            if (fryingRecipeSo.Input == inputKitchenObjectSO)
            {
                return fryingRecipeSo;
            }
        }

        return null;

    }
    private BurningRecipeSO GettingBurningRecipeSOWithInput(KitchenObjects inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSo in _burningRecipeSoArray)
        {
            if (burningRecipeSo.Input == inputKitchenObjectSO)
            {
                return burningRecipeSo;
            }
        }

        return null;

    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
}
