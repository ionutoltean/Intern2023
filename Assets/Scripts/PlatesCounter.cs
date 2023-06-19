using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    
    [SerializeField] private KitchenObjectSO PlateKitchenObjectSO;
    private float _spawnPlateTimer;
    private const float _spawnPlateTimerMax=4f;
    private int stackedPlates;
    private int stackedPlatesMax = 5;

    // Update is called once per frame
    
    

    void Update()
    {
        _spawnPlateTimer += Time.deltaTime;
        if (_spawnPlateTimer > _spawnPlateTimerMax && stackedPlates < stackedPlatesMax)
        {
            _spawnPlateTimer = 0f;
            stackedPlates++;
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }
    }
}
