using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject _hasProgressGameObject;
    private IHasProgress _hasProgress;
    [SerializeField] private Image _barImage;

    private void Start()
    {
        _hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();
        _hasProgress.OnProgressChanged += ProgressBarChanged;
        Hide();
    }

    private void ProgressBarChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
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