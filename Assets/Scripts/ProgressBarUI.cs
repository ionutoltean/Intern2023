using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter _cuttingCounter;
    [SerializeField] private Image _barImage;

    private void Start()
    {
        _cuttingCounter.OnProgressChanged += ProgressBarChanged;
        Hide();
    }

    private void ProgressBarChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        float progress = e.progressNormalized;
        _barImage.fillAmount = progress;
        if (progress == 0 || progress == 1)
            Hide();
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}