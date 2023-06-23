using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameOverUI : MonoBehaviour
{
    [FormerlySerializedAs("textCountdown")] [SerializeField]
    private TextMeshProUGUI textCount;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManagerStateChanged;
        gameObject.SetActive(false);
    }


    private void GameManagerStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            gameObject.SetActive(true);
            textCount.text = DeliveryManager.Instance.GetFinishedRecipeCount().ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}