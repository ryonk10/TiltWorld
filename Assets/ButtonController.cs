using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public stageController stageCon;
    public Button up, down, left, right;

    // Start is called before the first frame update
    public void ChageInteractableToFalse(string direction)
    {
        up.interactable = false;
        down.interactable = false;
        right.interactable = false;
        left.interactable = false;
        stageCon.StageTilt(direction);
    }

    public void ChangeInteractableToTrue()
    {
        up.interactable = true;
        down.interactable = true;
        right.interactable = true;
        left.interactable = true;
    }
}
