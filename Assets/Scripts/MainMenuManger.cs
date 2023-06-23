using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManger : MonoBehaviour
{
    [SerializeField] private Button quitButton;
    [SerializeField] private Button playButton;

    public void StartGameplay()
    {
        Loader.LoadScene(Loader.scenes.GameScene);
    }

    public void ExitGame()
    {
        Application.Quit(0);
    }
}
