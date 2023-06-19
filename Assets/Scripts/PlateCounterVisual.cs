using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private GameObject plateVisual;
    [SerializeField] private PlatesCounter platesCounter;
    private float plateOffset = 0.1f;
    private List<GameObject> currentPlates;
    private void Start()
    {
        platesCounter.OnPlateSpawned += OnPlatesSpawned;
    }

    private void Awake()
    {
        currentPlates = new List<GameObject>();
    }

    private void OnPlatesSpawned(object sender, EventArgs e)
    {
        GameObject spawnPlate = Instantiate(plateVisual, counterTopPoint);
        spawnPlate.transform.localPosition = new Vector3(0, plateOffset * currentPlates.Count, 0);
        currentPlates.Add(spawnPlate);

    }
}
