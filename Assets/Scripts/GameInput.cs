using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInterctAction;
    public event EventHandler OnInterctAltraNativeAction;
    public static GameInput Instance;

    private PlayerInputAction _playerInputAction;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Player.Enable();
    }

    private void OnEnable()
    {
        _playerInputAction.Player.Interact.performed += Intercat_PerFormed;
        _playerInputAction.Player.InteractAltraNative.performed += IntercatAltraNative_PerFormed;
    }

    private void IntercatAltraNative_PerFormed(InputAction.CallbackContext context)
    {
        OnInterctAltraNativeAction?.Invoke(this, EventArgs.Empty);

    }

    private void OnDisable()
    {
        _playerInputAction.Player.Interact.performed -= Intercat_PerFormed;
        _playerInputAction.Player.InteractAltraNative.performed -= IntercatAltraNative_PerFormed;
    }

    private void Intercat_PerFormed(InputAction.CallbackContext context)
    {
        // Debug.Log("intercation performed:" + context);
        OnInterctAction?.Invoke(this, EventArgs.Empty);

        //both are similar
        // if (OnInterctAction != null)
        // {
        //     OnInterctAction(this, EventArgs.Empty);
        // }
    }

    public Vector2 GetMovementVectorNorm()
    {
        Vector2 inputVector = _playerInputAction.Player.Move.ReadValue<Vector2>();

        //code without using new input system
        // if (Input.GetKey(KeyCode.W))
        // {
        //     inputVector.y = +1;
        // }
        // if (Input.GetKey(KeyCode.S))
        // {
        //     inputVector.y = -1;
        // }
        // if (Input.GetKey(KeyCode.A))
        // {
        //     inputVector.x = -1;
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     inputVector.x = +1;
        // }
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
