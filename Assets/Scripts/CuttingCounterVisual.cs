using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    [SerializeField] private CuttingCounter _containerCounter;
    private const string CHOP = "Cut";

    private void Start()
    {
        _containerCounter.OnCut += AnimateChop;
    }

    private void AnimateChop(object sender, EventArgs e)
    {
       _animator.SetTrigger(CHOP);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}