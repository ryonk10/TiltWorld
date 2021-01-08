using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stageController : MonoBehaviour
{
    public GameObject stage;
    public CharaController charaController;
    public ButtonController buttonController;
    public float speed;
    public float tiltDigree;
    public int size;
    public int charaEnterBlock { get; set; }
    public int charaExitBlock { get; set; }
    public bool charahitToWall { get; set; }
    public bool isCharaHitToIn { get; set; }

    private GameObject[] gameBlockPosition;
    private Vector3[,] baseBlockPosition;
    private Vector3[] destination;
    private List<ReturnGameClass> returnGameClass;
    private int direction;
    private int[,] isBlockExit;
    private bool blockMoveFlag;

    // Start is called before the first frame update
    private void Start()
    {
        charahitToWall = false;
        isCharaHitToIn = true;
        blockMoveFlag = false;
        returnGameClass = new List<ReturnGameClass>();
        //baseBlockの配置読み込み,isBlockExitを-1で初期化
        baseBlockPosition = new Vector3[size, size];
        isBlockExit = new int[size, size];
        for (var vertical = 0; vertical < size; vertical++)
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
        PrepareReturn();
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
            if (DoseStop() && charahitToWall)
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
        PrepareReturn();
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
                "x", tiltDigree,
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
                "x", -tiltDigree,
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
                "z", -tiltDigree,
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
                "z", tiltDigree,
                 "oncompletetarget", stage,
                "oncomplete", "SetBlockMoveFlag"));
        }
    }

    public void GameReset()
    {
        GameBack(0);
    }

    public void GameRetrun()
    {
        var lastIndex = returnGameClass.Count - 1;
        if (0 < lastIndex)
        {
            GameBack(lastIndex);
            returnGameClass.RemoveAt(lastIndex);
        }
    }

    public void PrepareReturn()
    {
        var returnPosi = new Vector3[size * size];
        var retrunIsExi = new int[size, size];
        //リターン位置の登録
        for (var i = 0; i < size * size; i++)
        {
            if (gameBlockPosition[i] != null)
            {
                returnPosi[i] = gameBlockPosition[i].transform.localPosition;
            }
            else
            {
                returnPosi[i] = new Vector3(-1, -1, -1);
            }
            retrunIsExi[i / size, i % size] = isBlockExit[i / size, i % size];
        }
        var tempRetrunGmaeClass = new ReturnGameClass(returnPosi,charaController.GetReturnCharaPosition(), retrunIsExi);
        returnGameClass.Add(tempRetrunGmaeClass);
    }

    private void GameBack(int backIndex)
    {
        charahitToWall = false;
        isCharaHitToIn = true;
        blockMoveFlag = false;
        buttonController.ChangeInteractableToTrue();
        stage.transform.rotation = Quaternion.Euler(0, this.transform.localRotation.eulerAngles.y, 0);
        var tempReturnGameClass = returnGameClass[backIndex];
        for (var i = 0; i < size * size; i++)
        {
            if (gameBlockPosition[i] != null)
            {
                gameBlockPosition[i].transform.localPosition = tempReturnGameClass.retrunGameBlockPosition[i];
                destination[i] = tempReturnGameClass.retrunGameBlockPosition[i];
            }
            isBlockExit[i / size, i % size] = tempReturnGameClass.returnIsExitBlock[i / size, i % size];
        }
        charaController.ReturnCharaPosition(tempReturnGameClass.returnCharaPosition);
    }

    private void SetBlockMoveFlag()
    {
        blockMoveFlag = true;
    }

    private void MoveCharaToDefaltPosition()
    {
        if (isCharaHitToIn)
        {
            charaController.charaExitBlockPosition = gameBlockPosition[charaEnterBlock].transform.localPosition;
        }
        else
        {
            charaController.charaExitBlockPosition = gameBlockPosition[charaExitBlock].transform.localPosition;
        }
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

    class ReturnGameClass
    {
        public Vector3[] retrunGameBlockPosition { get; set; }
        public Vector3 returnCharaPosition { get; set; }
        public int[,] returnIsExitBlock { get; set; }
        public ReturnGameClass(Vector3[] retrunPosi,Vector3 returnCharaPosi, int[,] isExblo)
        {
            this.retrunGameBlockPosition = retrunPosi;
            this.returnCharaPosition = returnCharaPosi;
            this.returnIsExitBlock = isExblo;
        }
    }
}