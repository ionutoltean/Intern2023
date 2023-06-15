using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    [SerializeField] private ContainerCounter _containerCounter;
    private const string OPEN_CLOSE = "OpenClose";

    private void Start()
    {
        _containerCounter.OnPlayerGrabbedObject += AnimateLid;
    }

    private void AnimateLid(object sender, EventArgs e)
    {
       _animator.SetTrigger(OPEN_CLOSE);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}