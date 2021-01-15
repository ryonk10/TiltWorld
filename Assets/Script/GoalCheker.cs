using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalCheker : MonoBehaviour
{
    public Camera mainCamera;
    public Camera subCamera;
    public GameObject quad2;
    public GameObject text;
    public GameObject clearForm;
    public GameObject button;
    public GameObject gameInfo;
    public CharaController charaController;
    public Animator endAnim;
    public Animator charaAnim;
    
    // Start is called before the first frame update
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "chara")
        {
            mainCamera.enabled = false;
            subCamera.enabled = true;
            text.SetActive(true);
            button.SetActive(false);
            clearForm.SetActive(true);
            quad2.SetActive(false);
            charaController.notGoal = false;
            gameInfo.SetActive(false);
            endAnim.SetTrigger("StartCutOut");
            charaAnim.SetFloat("Speed",0.2f);
        }
    }
}
