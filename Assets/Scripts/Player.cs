using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _rotateSpeed = 10;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private float _playerWidth = 1f;
    [SerializeField] private float _playerHeight = 1f;
    private float _interactDistance = 2f;
    private Vector3 _lastInteractDir;

    private bool isWalking;
    [SerializeField] private LayerMask _counterlayerMask;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        var inputVector = _gameInput.GetInputVector2Normalized();
        Vector3 moveDir;
        moveDir = inputVector != Vector2.zero ? new Vector3(inputVector.x, 0f, inputVector.y) : _lastInteractDir;
        _lastInteractDir = moveDir;
        if (Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, _interactDistance,_counterlayerMask))
        {
            Debug.Log(raycastHit.transform.name);
        }
    }

    private void HandleMovement()
    {
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
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.one * _playerHeight,
                _playerWidth, xDirection, moveDistance);
            if (canMove)
            {
                moveDir = xDirection;
            }
            //if can't move on x try on z 
            else
            {
                Vector3 zDirection = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position,
                    transform.position + Vector3.one * _playerHeight,
                    _playerWidth, zDirection, moveDistance);
                //can move on z ?
                if (canMove)
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
}