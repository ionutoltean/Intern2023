using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStateChangeEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangeEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] _fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] _burningRecipeSOArray;

    private float fryingTimer;
    private float burningTimer;
    private float neededMaximumTimer;
    private State currentState;

    private void Start()
    {
        currentState = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (currentState)
            {
                case State.Idle:
                {
                }
                    break;
                case State.Frying:
                {
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / neededMaximumTimer
                    });
                    if (fryingTimer > neededMaximumTimer)
                    {
                        burningTimer = 0f;
                        //fried
                        fryingTimer += Time.deltaTime;
                        KitchenObjectSO currentSo = GetKitchenObject().GetKitchenObjectSO();
                        GetKitchenObject().DestroySelf();
                        KitcheObject spawned =
                            KitcheObject.SpawnKitchenObject(GetOutputForInputFrying(currentSo), this);
                        currentState = State.Fried;
                        OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = currentState
                        });
                    }
                }
                    break;
                case State.Fried:
                {
                    burningTimer += Time.deltaTime;
                    OnProgressChanged.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / neededMaximumTimer
                    });
                    if (burningTimer > neededMaximumTimer)
                    {
                        //fried
                        fryingTimer += Time.deltaTime;
                        KitchenObjectSO currentSo = GetKitchenObject().GetKitchenObjectSO();
                        GetKitchenObject().DestroySelf();
                        KitcheObject spawned =
                            KitcheObject.SpawnKitchenObject(GetOutputForInputBurning(currentSo), this);
                        currentState = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = currentState
                        });
                        OnProgressChanged.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
                    break;
                case State.Burned:
                {
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            //object on the counter 
            if (player.HasKitchenObject() == false)
            {
                KitcheObject kitcheObject = GetKitchenObject();
                kitcheObject.SetKitchenObjectParent(player);
                currentState = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = currentState
                });
                OnProgressChanged.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
            else
            { // play has something
                if (player.GetKitchenObject().TryToGetPlate(out PlateKitchenObject plate))
                {
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        currentState = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = currentState
                        });
                        OnProgressChanged.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
                
            }
        }
        else
        {
            //no object
            if (player.HasKitchenObject() && HasIngredientForRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                KitcheObject kitcheObject = player.GetKitchenObject();
                kitcheObject.SetKitchenObjectParent(this);
                neededMaximumTimer = GetSecondsNeededMaximum(GetKitchenObject().GetKitchenObjectSO());
                currentState = State.Frying;
                fryingTimer = 0f;
                OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = currentState
                });
            }
        }
    }

    private KitchenObjectSO GetOutputForInputFrying(KitchenObjectSO input)
    {
        foreach (var recipe in _fryingRecipeSOArray)
        {
            if (recipe.input == input)
                return recipe.output;
        }

        return null;
    }

    private KitchenObjectSO GetOutputForInputBurning(KitchenObjectSO input)
    {
        foreach (var recipe in _burningRecipeSOArray)
        {
            if (recipe.input == input)
                return recipe.output;
        }

        return null;
    }

    private bool HasIngredientForRecipe(KitchenObjectSO input)
    {
        foreach (var recipe in _fryingRecipeSOArray)
        {
            if (recipe.input == input)
                return true;
        }

        return false;
    }

    private float GetSecondsNeededMaximum(KitchenObjectSO input)
    {
        foreach (var recipe in _fryingRecipeSOArray)
        {
            if (recipe.input == input)
                return recipe.fryingTimerMax;
        }

        Debug.LogError("Cuts NeededAreNot in the recipe array");
        return 0;
    }
}