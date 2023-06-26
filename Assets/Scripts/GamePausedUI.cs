using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenu;

    private void Start()
    {
        Hide();
        GameManager.Instance.OnGamePaused += Paused;
        GameManager.Instance.OnGamePaused += UnPaused;
        resumeButton.onClick.AddListener(() => { GameManager.Instance.TogglePause(); });
        mainMenu.onClick.AddListener(() => { Loader.LoadScene(Loader.scenes.MainMenuScene);
            Time.timeScale = 1f;
        });
    }


    private void UnPaused(object sender, EventArgs e)
    {
        Hide();
        Debug.Log("unpaused");
    }

    private void Paused(object sender, EventArgs e)
    {
        Debug.Log("showed");
        Show();
    }

    // Start is called before the first frame update
    private void Show()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}