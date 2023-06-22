using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _rotateSpeed = 10;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private float _playerWidth = 1f;
    [SerializeField] private float _playerHeight = 1f;
    private float _interactDistance = 2f;
    private Vector3 _lastInteractDir;
    private BaseCounter _selectedCounter;
    private bool isWalking;
    [SerializeField] private LayerMask _counterlayerMask;

    [SerializeField] private GameObject playerHoldPoint;

    private KitcheObject _kitcheObject;
    public event EventHandler<OnSelectedCounterEventArgs> OnSelectedCounterChange;
    public event EventHandler OnPickUp;

    public class OnSelectedCounterEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    public static Player Instance { get; private set; }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance was already set");
        }

        Instance = this;
    }

    private void Start()
    {
        _gameInput.OnInteractPerformed += PlayerInteracted;
        _gameInput.OnAlternateInteractPerformed += PlayerAlternateInteracted;
    }

    private void PlayerAlternateInteracted(object sender, EventArgs e)
    {
        if (_selectedCounter != null)
            _selectedCounter.InteractAlternate(this);
    }

    private void PlayerInteracted(object sender, EventArgs e)
    {
        if(GameManager.Instance.IsGamePlaying() ==false)return;
        if (_selectedCounter != null)
            _selectedCounter.Interact(this);
    }

    private void HandleInteraction()
    {
        if(GameManager.Instance.IsGamePlaying() ==false)return;
        var inputVector = _gameInput.GetInputVector2Normalized();
        Vector3 moveDir;
        moveDir = inputVector != Vector2.zero ? new Vector3(inputVector.x, 0f, inputVector.y) : _lastInteractDir;
        _lastInteractDir = moveDir;
        bool counterFound = Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, _interactDistance,
            _counterlayerMask);
        if (counterFound)
        {
            bool hasComponent = raycastHit.transform.TryGetComponent(out BaseCounter baseCounter);
            if (hasComponent)
            {
                if (baseCounter != _selectedCounter)
                {
                    ChangeSelectedCounter(baseCounter);
                }
            }

            if (hasComponent == false)
            {
                ChangeSelectedCounter(null);
            }
        }

        /////
        /// 
        if (counterFound == false)
        {
            ChangeSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        if(GameManager.Instance.IsGamePlaying() ==false)return;
        var inputVector = _gameInput.GetInputVector2Normalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir * _rotateSpeed, Time.deltaTime * _rotateSpeed);
        float moveDistance = Time.deltaTime * _moveSpeed;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.one * _playerHeight,
            _playerWidth, moveDir, moveDistance);
        if (canMove == false)
        {
            // cant move diagonally 
            Vector3 xDirection = new Vector3(moveDir.x, 0, 0);
            // can move only on x ? 
            if (xDirection != Vector3.zero)
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.one * _playerHeight,
                    _playerWidth, xDirection, moveDistance);
            if (canMove && moveDir.x != 0)
            {
                moveDir = xDirection;
            }
            //if can't move on x try on z 
            else
            {
                Vector3 zDirection = new Vector3(0, 0, moveDir.z);
                if (zDirection != Vector3.zero)
                    canMove = !Physics.CapsuleCast(transform.position,
                        transform.position + Vector3.one * _playerHeight,
                        _playerWidth, zDirection, moveDistance);
                //can move on z ?
                if (canMove && moveDir.z != 0)
                {
                    moveDir = zDirection;
                }
            }
        }

            var step = moveDir * moveDistance;
        if (canMove)
        {
            transform.position += step;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public void ChangeSelectedCounter(BaseCounter baseCounter)
    {
        _selectedCounter = baseCounter;
        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterEventArgs
        {
            selectedCounter = _selectedCounter
        });
    }

    public Transform GetCounterTopPoint()
    {
        return playerHoldPoint.transform;
    }

    public KitcheObject GetKitchenObject()
    {
        return _kitcheObject;
    }

    public void SetKitchenObject(KitcheObject kitcheObject)
    {
        this._kitcheObject = kitcheObject;
        if(kitcheObject!=null)
            OnPickUp?.Invoke(this,EventArgs.Empty);
    }

    public bool HasKitchenObject()
    {
        return _kitcheObject != null;
    }

    public void ClearKitchenObject()
    {
        _kitcheObject = null;
    }
}