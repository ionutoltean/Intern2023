using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    public enum scenes{
        GameScene,
        LoadingScene,
        MainMenuScene,
    }

    private static scenes sceneToLoad;
    public static void LoadScene(scenes scene)
    {
        Loader.sceneToLoad = scene;
        SceneManager.LoadScene(scenes.LoadingScene.ToString());
     
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene( Loader.sceneToLoad.ToString());
    }
}
