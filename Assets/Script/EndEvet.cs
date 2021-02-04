using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndEvet: MonoBehaviour
{ 
    public GameObject endCanvas;
    public Animator anim;
    public Camera subCamera;
    public GameObject quad2;
    public GameObject text;
    public GameObject clearForm;
    public GameObject button;
    public GameObject gameInfo;
    public Animator endAnim;

    public void OnGoal()
    {
        Camera.main.enabled = false;
        subCamera.enabled = true;
        text.SetActive(true);
        button.SetActive(false);
        clearForm.SetActive(true);
        quad2.SetActive(false);
        GameObject.FindGameObjectWithTag("Chara").GetComponent<CharaController>().notGoal = false;
        gameInfo.SetActive(false);
        endAnim.SetTrigger("StartCutOut");
        StartCoroutine(ShowEndCanvas());

    }

    private IEnumerator ShowEndCanvas()
    {
        yield return null;
        while(!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            yield return null;
        }
        endCanvas.SetActive(true);
    }
}
