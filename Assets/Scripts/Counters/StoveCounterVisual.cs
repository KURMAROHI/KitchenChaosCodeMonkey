using System;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject _stoveOnGameObject;
    [SerializeField] private GameObject _particlesGameObject;
    [SerializeField] private StoveCounter _stoveCounter;

    private void Start()
    {
        _stoveCounter.OnStateChanged += OnStoveCounter_StateChanged;
    }


    private void OnDisable()
    {
        _stoveCounter.OnStateChanged -= OnStoveCounter_StateChanged;

    }
    private void OnStoveCounter_StateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying;
        _stoveOnGameObject.SetActive(showVisual);
        _particlesGameObject.SetActive(showVisual);
    }

}
