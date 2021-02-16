using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject goalCanvas;
   public void ShowGoalForm()
    {
        goalCanvas.SetActive(true);
    }
    public void CloseGoalForm()
    {
        goalCanvas.SetActive(false);
    }
}
