using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button up, down, left, right, reSet, reTrun;
    public Text MoveCountText;

    private StageController stageController;

    private void Start()
    {
        stageController = GameObject.FindGameObjectWithTag("Stage").GetComponent<StageController>();
    }

    // Start is called before the first frame update
    public void ChageInteractableToFalse(string direction)
    {
        var moveCount = int.Parse(MoveCountText.text)+1;
        MoveCountText.text = moveCount.ToString();
        up.interactable = false;
        down.interactable = false;
        right.interactable = false;
        left.interactable = false;
        reSet.interactable = false;
        reTrun.interactable = false;
        stageController.StageTilt(direction);
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
        MoveCountText.text = "0";
        stageController.GameReset();
    }

    public void GameReturn()
    {
        var moveCount = int.Parse(MoveCountText.text)-1;
        if (moveCount < 0)
        {
            moveCount = 0;
        }
        MoveCountText.text = moveCount.ToString();
        stageController.GameRetrun();
    }
}