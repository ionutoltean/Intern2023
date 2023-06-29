using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveLight;
    [SerializeField] private GameObject particle;
    [SerializeField] private GameObject warningImage;

    [SerializeField] private StoveCounter _stoveCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        _stoveCounter.OnStateChanged += StateChanged;
        _stoveCounter.OnWarning += ShowWarning;
        warningImage.SetActive(false);
    }

    private void ShowWarning(object sender, EventArgs e)
    {
        warningImage.SetActive(true);
    }

    private void StateChanged(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried; 
        stoveLight.SetActive(showVisual);
        particle.SetActive(showVisual);
        if(e.state !=StoveCounter.State.Fried)
            warningImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
