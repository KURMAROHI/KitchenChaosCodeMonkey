using System;
using Unity.VisualScripting;
using UnityEngine;

public class SelctedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private GameObject[] _visualGameObjectS;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChange += PlayerOnSelected_CounterChange;
    }

    private void OnDisable()
    {
        Player.Instance.OnSelectedCounterChange -= PlayerOnSelected_CounterChange;
    }

    private void PlayerOnSelected_CounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        if (e.SelectedBaseCounter == _baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in _visualGameObjectS)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in _visualGameObjectS)
        {
            visualGameObject.SetActive(false);
        }

    }
}
