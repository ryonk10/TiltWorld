﻿using System.Linq;
using UnityEngine;

public class stageController : MonoBehaviour
{
   
    public GameObject stage;
    public CharaController charaController;
    public float speed;
    public int size;
    public int charaExitBlock { get; set; }
    public bool charahitToWall { get; set; } = false;

    private GameObject[] gameBlockPosition;
    private Vector3[,] baseBlockPosition;
    private Vector3[] destination;
    private int direction;
    private int[,] isBlockExit;
    private bool blockMoveFlag = false;

    // Start is called before the first frame update
    private void Start()
    {
        //baseBlockの配置読み込み,isBlockExitを-1で初期化
        baseBlockPosition = new Vector3[size, size];
        isBlockExit = new int[size, size];
        for (var vertical=0;vertical<size;vertical++)
        {
            for (var horizon = 0; horizon < size; horizon++)
            {
                baseBlockPosition[vertical, horizon] = new Vector3(horizon, 0, vertical);
                isBlockExit[vertical, horizon] = -1;
            }
        }
        //
        //blockの配置読み込み
        //
        gameBlockPosition = new GameObject[size * size];
        destination = new Vector3[size * size];
        var blocks = GameObject.FindGameObjectsWithTag("block");
        for (var i = 0; i < blocks.Length; i++)
        {
            var index = int.Parse(blocks[i].name);
            gameBlockPosition[index] = blocks[i];
            destination[index] = blocks[i].transform.localPosition;
            isBlockExit[index / size, index % size] = index;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (blockMoveFlag)
        {
            //ブロック移動
            float step = speed * Time.deltaTime;
            for (var index = 0; index < size * size; index++)
            {
                if (gameBlockPosition[index] != null)
                {
                    gameBlockPosition[index].transform.localPosition = Vector3.MoveTowards(gameBlockPosition[index].transform.localPosition, destination[index], step);
                }
            }
            //
            //ステージを戻す
            //
            if (DoseStop()&&charahitToWall)
            {
                blockMoveFlag = false;
                charahitToWall = false;
                if (direction == 0)
                {
                    iTween.RotateTo(stage, iTween.Hash("x", 0f,
                        "oncompletetarget", stage,
                "oncomplete", "MoveCharaToDefaltPosition"));
                }
                else if (direction == 1)
                {
                    iTween.RotateTo(stage, iTween.Hash("x", 0f,
                        "oncompletetarget", stage,
                "oncomplete", "MoveCharaToDefaltPosition"));
                }
                else if (direction == 2)
                {
                    iTween.RotateTo(stage, iTween.Hash("z", 0f,
                        "oncompletetarget", stage,
                "oncomplete", "MoveCharaToDefaltPosition"));
                }
                else if (direction == 3)
                {
                    iTween.RotateTo(stage, iTween.Hash("z", 0f,
                        "oncompletetarget", stage,
                "oncomplete", "MoveCharaToDefaltPosition"));
                }
            }
        }
    }

    public void StageTilt(string direction)
    {
        if (direction == "u")
        {
            this.direction = 0;
            //それぞれのブロックの移動位置設定
            for (int vertical = size - 2; 0 <= vertical; vertical--)
            {
                for (int horizon = 0; horizon < size; horizon++)
                {
                    if (isBlockExit[vertical, horizon] == -1)
                    {
                        continue;
                    }
                    var t = vertical + 1;
                    var number = isBlockExit[vertical, horizon];
                    while (t < size)
                    {
                        if (0 <= isBlockExit[t, horizon])
                        {
                            break;
                        }
                        else
                        {
                            isBlockExit[t - 1, horizon] = -1;
                            isBlockExit[t, horizon] = number;
                            t++;
                        }
                    }
                    destination[number] = baseBlockPosition[t - 1, horizon];
                }
            }
            iTween.RotateTo(stage, iTween.Hash(
                "x", 45f,
                "oncompletetarget", stage,
                "oncomplete", "SetBlockMoveFlag"));
        }
        else if (direction == "d")
        {
            this.direction = 1;
            //それぞれのブロックの移動位置設定
            for (int vertical = 1; vertical < size; vertical++)
            {
                for (int horizon = 0; horizon < size; horizon++)
                {
                    if (isBlockExit[vertical, horizon] == -1)
                    {
                        continue;
                    }
                    var t = vertical - 1;
                    var number = isBlockExit[vertical, horizon];
                    while (0 <= t)
                    {
                        if (0 <= isBlockExit[t, horizon])
                        {
                            break;
                        }
                        else
                        {
                            isBlockExit[t + 1, horizon] = -1;
                            isBlockExit[t, horizon] = number;
                            t--;
                        }
                    }
                    destination[number] = baseBlockPosition[t + 1, horizon];
                }
            }
            iTween.RotateTo(stage, iTween.Hash(
                "x", -45f,
                 "oncompletetarget", stage,
                "oncomplete", "SetBlockMoveFlag"));
        }
        else if (direction == "r")
        {
            this.direction = 2;
            //それぞれのブロックの移動位置設定
            for (int horizon = size - 2; 0 <= horizon; horizon--)
            {
                for (int vertical = 0; vertical < size; vertical++)
                {
                    if (isBlockExit[vertical, horizon] == -1)
                    {
                        continue;
                    }
                    var t = horizon + 1;
                    var number = isBlockExit[vertical, horizon];
                    while (t < size)
                    {
                        if (0 <= isBlockExit[vertical, t])
                        {
                            break;
                        }
                        else
                        {
                            isBlockExit[vertical, t - 1] = -1;
                            isBlockExit[vertical, t] = number;
                            t++;
                        }
                    }
                    destination[number] = baseBlockPosition[vertical, t - 1];
                }
            }
            iTween.RotateTo(stage, iTween.Hash(
                "z", -45f,
                 "oncompletetarget", stage,
                "oncomplete", "SetBlockMoveFlag"));
        }
        else if (direction == "l")
        {
            this.direction = 3;
            //それぞれのブロックの移動位置設定
            for (int horizon = 1; horizon < size; horizon++)
            {
                for (int vertical = 0; vertical < size; vertical++)
                {
                    if (isBlockExit[vertical, horizon] == -1)
                    {
                        continue;
                    }
                    var t = horizon - 1;
                    var number = isBlockExit[vertical, horizon];
                    while (0 <= t)
                    {
                        if (0 <= isBlockExit[vertical, t])
                        {
                            break;
                        }
                        else
                        {
                            isBlockExit[vertical, t + 1] = -1;
                            isBlockExit[vertical, t] = number;
                            t--;
                        }
                    }
                    destination[number] = baseBlockPosition[vertical, t + 1];
                }
            }
            iTween.RotateTo(stage, iTween.Hash(
                "z", 45f,
                 "oncompletetarget", stage,
                "oncomplete", "SetBlockMoveFlag"));
        }
    }

    private void SetBlockMoveFlag()
    {
        blockMoveFlag = true;
    }

    private void MoveCharaToDefaltPosition()
    {
        charaController.charaExitBlockPosition = gameBlockPosition[charaExitBlock].transform.position;
        charaController.doseStageReturn = true;
    }

    private bool DoseStop()
    {
        for (var index = 0; index < size * size; index++)
        {
            if (gameBlockPosition[index] == null)
            {
                continue;
            }
            if (gameBlockPosition[index].transform.localPosition != destination[index])
            {
                return false;
            }
        }
        return true;
    }
}