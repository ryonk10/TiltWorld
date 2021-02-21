using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoController : MonoBehaviour
{
    public GameObject endForm;
    public Text textEndInfo;
    public Text textClearMoveCount;
    public Text textUserInfo;
    public Text moveCountText;

    public void UpMoveCount()
    {
        var moveCount = int.Parse(moveCountText.text) + 1;
        moveCountText.text = moveCount.ToString();
    }
    public void DownMoveCount()
    {
        var moveCount = int.Parse(moveCountText.text) - 1;
        if (moveCount < 0)
        {
            moveCount = 0;
        }
        moveCountText.text = moveCount.ToString();
    }
    public void ResetMoveCount()
    {
        moveCountText.text = "0";
    }
    public void SetTextEndIfo(string text)
    {
        textEndInfo.text = text;
    }
    public void SetTextUserInfo(string text)
    {
        textUserInfo.text = text;
    }
    public void ShowEndForm()
    {
        textClearMoveCount.text = moveCountText.text + "回でクリア！！";
        endForm.SetActive(true);
    }
    public void CLoseEndForm()
    {
        endForm.SetActive(false);
    }
}
