using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private Transform _plateVisual;
    [SerializeField] private PlatesCounter _platesCounter;

    private List<GameObject> _plateVisualGameObjectList;
    private void Start()
    {
        _platesCounter.OnPlatesSpawned += PlatesCounter_OnPlateSpawned;
        _platesCounter.OnPlatesRemoved += PlatesCounter_OnPlateRemoved;
        _plateVisualGameObjectList = new List<GameObject>();
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plateVisualGameObject = _plateVisualGameObjectList[_plateVisualGameObjectList.Count - 1];
        _plateVisualGameObjectList.Remove(plateVisualGameObject);
        Destroy(plateVisualGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform plate = Instantiate(_plateVisual, _counterTopPoint);
        float plateOffetY = 0.1f;
        plate.localPosition = new Vector3(0f, plateOffetY * _plateVisualGameObjectList.Count, 0f);
        _plateVisualGameObjectList.Add(plate.gameObject);
    }


}
