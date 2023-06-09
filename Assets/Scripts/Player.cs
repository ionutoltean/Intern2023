using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _rotateSpeed = 10;
    [SerializeField] private GameInput _gameInput;

    private bool isWalking;

    // Update is called once per frame
    void Update()
    {
        var inputVector = _gameInput.GetInputVector2Normalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
         isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir * _rotateSpeed, Time.deltaTime * _rotateSpeed);
        var step = moveDir * (Time.deltaTime * _moveSpeed);
        transform.position += step;
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}