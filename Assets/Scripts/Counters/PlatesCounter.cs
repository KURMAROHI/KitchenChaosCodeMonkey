using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjects plateKictchenObjectSO;
    private float _spawnPlateTimer;
    private float _spwanPlateTimerMax = 4f;
    private int _palteSpawnAmount;
    private int _platesSpawnAmountMax = 4;

    public event EventHandler OnPlatesSpawned;
    public event EventHandler OnPlatesRemoved;


    private void Update()
    {
        _spawnPlateTimer += Time.deltaTime;
        if (_spawnPlateTimer > _spwanPlateTimerMax)
        {
            _spawnPlateTimer = 0;
            if (_palteSpawnAmount < _platesSpawnAmountMax)
            {
                _palteSpawnAmount++;
                OnPlatesSpawned?.Invoke(this, EventArgs.Empty);
            }
            // KitchenObject.SpawnKitchenObject(plateKictchenObjectSO, this);
        }
    }
    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            if(_palteSpawnAmount>0)
            {
                _palteSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKictchenObjectSO,player);
                OnPlatesRemoved?.Invoke(this,EventArgs.Empty);
            }
        }
    }
}
