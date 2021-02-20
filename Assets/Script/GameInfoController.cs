using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoController : MonoBehaviour
{
    public GameObject EndForm;
    public Text MoveCountText;

    public void UpMoveCount()
    {
        var moveCount = int.Parse(MoveCountText.text) + 1;
        MoveCountText.text = moveCount.ToString();
    }
    public void DownMoveCount()
    {
        var moveCount = int.Parse(MoveCountText.text) - 1;
        if (moveCount < 0)
        {
            moveCount = 0;
        }
        MoveCountText.text = moveCount.ToString();
    }
    public void ResetMoveCount()
    {
        MoveCountText.text = "0";
    }
    public void ShowEndForm()
    {
        EndForm.SetActive(true);
    }
    public void CLoseEndForm()
    {
        EndForm.SetActive(false);
    }
}
