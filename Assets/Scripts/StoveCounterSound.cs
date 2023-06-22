using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource _audioSource;
   [SerializeField] private StoveCounter stoveCounter;

   private void Start()
   {
       stoveCounter.OnStateChanged += StoveStateChanged;
   }

   private void StoveStateChanged(object sender, StoveCounter.OnStateChangeEventArgs e)
   {
       bool isPlaying = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
       if (isPlaying)
       {
           _audioSource.Play();
       }
       else
       {
           _audioSource.Pause();
       }
   }

   private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
