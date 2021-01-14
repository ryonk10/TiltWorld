using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndEvet: MonoBehaviour
{
    public GameObject endCanvas;

    // Start is called before the first frame update
    public void End()
    {
        endCanvas.SetActive(true);
    }
    public void ReStartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

}
