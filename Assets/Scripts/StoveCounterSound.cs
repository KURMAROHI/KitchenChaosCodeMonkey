using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StoveCounterSound : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private StoveCounter _stoveCounter;

    private void Start()
    {
        _audioSource =GetComponent<AudioSource>();
        _stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void OnDisable()
    {
        _stoveCounter.OnStateChanged -= StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool isPlaySound=e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying;
        if(isPlaySound)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Pause();
        }
    }
}
