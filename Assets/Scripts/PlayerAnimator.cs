

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private const string ANIMATION_TRIGGER = "IsWalking";
    [SerializeField]
    private Player _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
       _animator.SetBool(ANIMATION_TRIGGER,_player.IsWalking());
    }
}
