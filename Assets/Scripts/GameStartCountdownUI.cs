using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCountdown;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManagerStateChanged;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        textCountdown.text = GameManager.Instance.GetCountdownToStartTimer().ToString("#");
    }

    private void GameManagerStateChanged(object sender, EventArgs e)
    {
        gameObject.SetActive(GameManager.Instance.IsGameCountingDown());
    }

  
}
