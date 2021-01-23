using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private AsyncOperation scene;

    public void NextSceneLoad(string sceneName)
    {
        scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
    }

    public void NextSceneStart()
    {
        scene.allowSceneActivation = true;
    }
}
