using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalCheker : MonoBehaviour
{
    public GameObject quad2;

    public Camera mainCamera;
    public Camera subCamera;
    public GameObject text;
    // Start is called before the first frame update
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "chara")
        {
            quad2.SetActive(false);
            mainCamera.enabled = false;
            subCamera.enabled = true;
            text.SetActive(true);
        }
    }
}
