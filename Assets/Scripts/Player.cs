using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private LayerMask _counterLayerMask;
    [SerializeField] private Transform _playerHoldingPoint;

    private bool _isWalking;
    private Vector3 _lastInteractionDir;
    private BaseCounter _selectedBaseCounter;
    private KitchenObject _kitchenObject;


    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter SelectedBaseCounter;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("more than one player instance");
        }
        Instance = this;
    }

    private void OnEnable()
    {
        GameInput.Instance.OnInterctAction += GameInput_OnInterctAction;
        GameInput.Instance.OnInterctAltraNativeAction += GameInput_OnAltranateAction;
    }


    private void Update()
    {
        HandleMovement();
        HandleIntercation();
    }
    private void OnDisable()
    {
        GameInput.Instance.OnInterctAction -= GameInput_OnInterctAction;
        GameInput.Instance.OnInterctAltraNativeAction -= GameInput_OnAltranateAction;
    }

    private void GameInput_OnInterctAction(object sender, EventArgs e)
    {
        if (_selectedBaseCounter != null)
        {
            _selectedBaseCounter.Interact(this);
        }
    }
    private void GameInput_OnAltranateAction(object sender, EventArgs e)
    {

        if (_selectedBaseCounter != null)
        {
            _selectedBaseCounter.InteractAltranate(this);
        }
    }

    private void HandleIntercation()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNorm();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            _lastInteractionDir = moveDir;
        }
        float intercationDist = 2f;


        if (Physics.Raycast(transform.position, _lastInteractionDir, out RaycastHit hitInfo, intercationDist, _counterLayerMask))
        {
            if (hitInfo.transform.TryGetComponent(out BaseCounter clearCounter))
            {
                if (clearCounter != _selectedBaseCounter)
                {
                    SetSelctedCounter(clearCounter);
                }
            }
            else
            {
                SetSelctedCounter(null);
            }
        }
        else
        {
            SetSelctedCounter(null);
        }

    }


    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNorm();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        _isWalking = moveDir != Vector3.zero;

        float moveDistance = Time.deltaTime * _moveSpeed;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        // bool canMove = Physics.Raycast(transform.position, moveDir, playerSize);
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                //cannot move on x but checking in Z Direction
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move in Any Direction
                }
            }

        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime);
    }

    private void SetSelctedCounter(BaseCounter baseCounter)
    {
        this._selectedBaseCounter = baseCounter;
        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs { SelectedBaseCounter = baseCounter });
    }

    public bool IsWalking()
    {
        return _isWalking;
    }

    public Transform GetKitchenObjectFollowTransFrom()
    {
        return _playerHoldingPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
