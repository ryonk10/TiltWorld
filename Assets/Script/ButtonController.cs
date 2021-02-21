using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button up, down, left, right, reSet, reTrun;

    public void ButtonToFalse()
    {
        up.interactable = false;
        down.interactable = false;
        right.interactable = false;
        left.interactable = false;
        reSet.interactable = false;
        reTrun.interactable = false;
    }
    public void ButtonToTure()
    {
        up.interactable = true;
        down.interactable = true;
        right.interactable = true;
        left.interactable = true;
        reSet.interactable = true;
        reTrun.interactable = true;
    }

    public void MoveButtonToTrue()
    {
        up.interactable = true;
        down.interactable = true;
        right.interactable = true;
        left.interactable = true;
    }
    public void MoveButtonToFalse()
    {
        up.interactable = false;
        down.interactable = false;
        right.interactable = false;
        left.interactable = false;
    }

    public void ResetReturnToTure()
    {
        reSet.interactable = true;
        reTrun.interactable = true;
    }
    public void ResetReturnToFalse()
    {
        reSet.interactable = false;
        reTrun.interactable = false;
    }
}