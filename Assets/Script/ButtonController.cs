using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button up, down, left, right, reSet, reTrun;

    private stageController stageCon;

    private void Start()
    {
        stageCon = GameObject.FindGameObjectWithTag("Stage").GetComponent<stageController>();
    }

    // Start is called before the first frame update
    public void ChageInteractableToFalse(string direction)
    {
        up.interactable = false;
        down.interactable = false;
        right.interactable = false;
        left.interactable = false;
        reSet.interactable = false;
        reTrun.interactable = false;
        stageCon.StageTilt(direction);
    }

    public void ChangeInteractableToTrue()
    {
        up.interactable = true;
        down.interactable = true;
        right.interactable = true;
        left.interactable = true;
    }

    public void ResetReturnToTure()
    {
        reSet.interactable = true;
        reTrun.interactable = true;
    }

    public void GameReset()
    {
        stageCon.GameReset();
    }

    public void GameReturn()
    {
        stageCon.GameRetrun();
    }
}