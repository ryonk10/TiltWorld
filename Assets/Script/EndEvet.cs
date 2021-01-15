using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndEvet: MonoBehaviour
{
    public GameObject endCanvas;
    public Animator anim;

    // Start is called before the first frame update

    private void Update()
    {
        if (1 <= anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            End();
        }
    }
    public void End()
    {
        endCanvas.SetActive(true);
    }
    public void ReStartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

}
