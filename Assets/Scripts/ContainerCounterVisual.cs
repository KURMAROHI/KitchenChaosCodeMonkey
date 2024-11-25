using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter _containetCounter;
    private Animator _animator;
    private const string Open_Close = "OpenClose";


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _containetCounter.OnPlayerGranbObject += ContainerCounter_OnPlayerGrabObject;
    }


    private void OnDisable()
    {
        _containetCounter.OnPlayerGranbObject -= ContainerCounter_OnPlayerGrabObject;

    }
    private void ContainerCounter_OnPlayerGrabObject(object sender, EventArgs e)
    {
        _animator.SetTrigger(Animator.StringToHash(Open_Close));
    }
}
