using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public List<ReturnGameClass> returnGameClass;
    public float speed;
    public float tiltDigree;
    public int verticalSize;
    public int horizonSize;
    public bool isEditMode;

    public StageFaze stageFaze { get; set; }
    public int charaEnterBlock { get; set; }
    public int charaExitBlock { get; set; }
    public int direction { get; set; }
    public bool charahitToWall { get; set; }
    public bool isCharaHitToIn { get; set; }

    private GameObject[] gameBlockPosition;
    private GameObject chara;
    private ButtonController buttonController;
    private CharaController charaController;
    private Vector3[,] baseBlockPosition;
    private Vector3[] destination;

    //-1=空白　-2=固定ブロック
    private int[,] isBlockExit;

    private Animator animator;

    public enum StageFaze
    {
        IDLE,
        PREPARE_TILT,
        STAGE_TILT,
        BLOCK_MOVE,
        STAGE_RETURN,
        CHARA_MOVE_DEFAULT,
        GOAL
    }

    // Start is called before the first frame update
    private void Start()
    {
        stageFaze = StageFaze.IDLE;
        if (isEditMode)
        {
            CreatePositonBlock();
            CreatePositionGloal();
        }
        else
        {
            StageInitialize();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (stageFaze == StageFaze.BLOCK_MOVE)
        {
            //ブロック移動
            float step = speed * Time.deltaTime;
            for (var index = 0; index < verticalSize * horizonSize; index++)
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
                stageFaze = StageFaze.STAGE_RETURN;
                chara.transform.parent = this.transform.Find("block");
                charahitToWall = false;
                if (direction == 1)
                {
                    iTween.RotateTo(this.gameObject, iTween.Hash("x", 0f,
                        "oncompletetarget", this.gameObject,
                "oncomplete", "ToFazeCHARA_MOVE_DEFAULT"));
                }
                else if (direction == 2)
                {
                    iTween.RotateTo(this.gameObject, iTween.Hash("x", 0f,
                        "oncompletetarget", this.gameObject,
                "oncomplete", "ToFazeCHARA_MOVE_DEFAULT"));
                }
                else if (direction == 3)
                {
                    iTween.RotateTo(this.gameObject, iTween.Hash("z", 0f,
                        "oncompletetarget", this.gameObject,
                "oncomplete", "ToFazeCHARA_MOVE_DEFAULT"));
                }
                else if (direction == 4)
                {
                    iTween.RotateTo(this.gameObject, iTween.Hash("z", 0f,
                        "oncompletetarget", this.gameObject,
                "oncomplete", "ToFazeCHARA_MOVE_DEFAULT"));
                }
            }
        }
    }

    public void CreatePositonBlock()
    {
        var tempBlock = (GameObject)Resources.Load("PositionBlock");
        tempBlock.tag = "PositionBlock";
        var parent = this.transform.Find("block");
        for (var vertical = 0; vertical < verticalSize; vertical++)
        {
            for (var horizon = 0; horizon < horizonSize; horizon++)
            {
                var positionBlock = Instantiate(tempBlock, parent);
                positionBlock.transform.localPosition = new Vector3(horizon, -0.8f, vertical);
                positionBlock.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void CreatePositionGloal()
    {
        var tempBlock = (GameObject)Resources.Load("PositionBlock");
        tempBlock.tag = "PositionGoal";
        var parent = this.transform.Find("Wall");
        for (var vertical = -1; vertical < verticalSize - 1; vertical++)
        {
            var positionGoalL = Instantiate(tempBlock, parent);
            positionGoalL.transform.localPosition = new Vector3(-2, -0.8f, vertical);
            positionGoalL.transform.localScale = new Vector3(1, 1, 1);
            var positionGoalR = Instantiate(tempBlock, parent);
            positionGoalR.transform.localPosition = new Vector3(horizonSize - 1, -0.8f, vertical);
            positionGoalR.transform.localScale = new Vector3(1, 1, 1);
        }
        for (var horizon = -1; horizon < horizonSize - 1; horizon++)
        {
            var positionGoalL = Instantiate(tempBlock, parent);
            positionGoalL.transform.localPosition = new Vector3(horizon, -0.8f, -2);
            positionGoalL.transform.localScale = new Vector3(1, 1, 1);
            var positionGoalR = Instantiate(tempBlock, parent);
            positionGoalR.transform.localPosition = new Vector3(horizon, -0.8f, verticalSize - 1);
            positionGoalR.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void StageInitialize()
    {
        charahitToWall = false;
        isCharaHitToIn = true;
        stageFaze = StageFaze.IDLE;
        chara = GameObject.FindGameObjectWithTag("Chara");
        charaController = chara.GetComponent<CharaController>();
        buttonController = GameObject.FindGameObjectWithTag("ButtonController").GetComponent<ButtonController>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("SetMove");
        returnGameClass = new List<ReturnGameClass>();
        //baseBlockの配置読み込み,isBlockExitを-1で初期化
        baseBlockPosition = new Vector3[verticalSize, horizonSize];
        isBlockExit = new int[verticalSize, horizonSize];
        for (var vertical = 0; vertical < verticalSize; vertical++)
        {
            for (var horizon = 0; horizon < horizonSize; horizon++)
            {
                baseBlockPosition[vertical, horizon] = new Vector3(horizon, 0, vertical);
                isBlockExit[vertical, horizon] = -1;
            }
        }
        //
        //blockの配置読み込み
        //
        gameBlockPosition = new GameObject[verticalSize * horizonSize];
        destination = new Vector3[verticalSize * horizonSize];
        var blocks = GameObject.FindGameObjectsWithTag("block");
        foreach (var block in blocks)
        {
            var index = (int)(block.transform.localPosition.z * horizonSize + block.transform.localPosition.x);
            if (block.gameObject.name.Contains("F"))
            {
                block.gameObject.name = index.ToString() + "F";
                isBlockExit[index / horizonSize, index % horizonSize] = -2;
                continue;
            }
            else
            {
                string type;
                if (block.gameObject.name.Contains("H"))
                {
                    type ="H";
                }
                else if (block.gameObject.name.Contains("L"))
                {
                    type = "L";
                }
                else if (block.gameObject.name.Contains("Obs"))
                {
                    type = "Obs";
                }
                else
                {
                    type = "N";
                }
                block.gameObject.name = index.ToString() + type;
                gameBlockPosition[index] = block;
                destination[index] = block.transform.localPosition;
                isBlockExit[index / horizonSize, index % horizonSize] = index;
            }
        }
        chara.GetComponent<Rigidbody>().useGravity = true;
        chara.GetComponent<Rigidbody>().drag = 1;
    }

    public void StageTilt(string direction)
    {
        animator.SetTrigger("SetIdle");
        PrepareReturn();
        stageFaze = StageFaze.PREPARE_TILT;
        if (direction == "u")
        {
            this.direction = 1;
            //それぞれのブロックの移動位置設定
            //一番上の列はそれ以上前には進まないから２列目から調査
            for (int vertical = verticalSize - 2; 0 <= vertical; vertical--)
            {
                for (int horizon = 0; horizon < horizonSize; horizon++)
                {
                    if (isBlockExit[vertical, horizon] < 0)
                    {
                        continue;
                    }
                    var t = vertical + 1;
                    var number = isBlockExit[vertical, horizon];
                    //自分より上が空白かを調べる
                    while (t < verticalSize)
                    {
                        if (isBlockExit[t, horizon] != -1)
                        {
                            break;
                        }
                        else
                        {
                            //現在地が空白であり、現在地をとりあえず動かす予定のブロックの目標地点として登録して、さらに前が空白かを調べる
                            isBlockExit[t - 1, horizon] = -1;
                            isBlockExit[t, horizon] = number;
                            t++;
                        }
                    }
                    destination[number] = baseBlockPosition[t - 1, horizon];
                }
            }
            iTween.RotateTo(this.gameObject, iTween.Hash(
                "x", tiltDigree,
                "oncompletetarget", this.gameObject,
                "oncomplete", "ToFazeSTAGE_TILT"));
        }
        else if (direction == "d")
        {
            this.direction = 2;
            //それぞれのブロックの移動位置設定
            for (int vertical = 1; vertical < verticalSize; vertical++)
            {
                for (int horizon = 0; horizon < horizonSize; horizon++)
                {
                    if (isBlockExit[vertical, horizon] < 0)
                    {
                        continue;
                    }
                    var t = vertical - 1;
                    var number = isBlockExit[vertical, horizon];
                    while (0 <= t)
                    {
                        if (isBlockExit[t, horizon] != -1)
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
            iTween.RotateTo(this.gameObject, iTween.Hash(
                "x", -tiltDigree,
                 "oncompletetarget", this.gameObject,
                "oncomplete", "ToFazeSTAGE_TILT"));
        }
        else if (direction == "r")
        {
            this.direction = 3;
            //それぞれのブロックの移動位置設定
            for (int horizon = horizonSize - 2; 0 <= horizon; horizon--)
            {
                for (int vertical = 0; vertical < verticalSize; vertical++)
                {
                    if (isBlockExit[vertical, horizon] < 0)
                    {
                        continue;
                    }
                    var t = horizon + 1;
                    var number = isBlockExit[vertical, horizon];
                    while (t < horizonSize)
                    {
                        if (isBlockExit[vertical, t] != -1)
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
            iTween.RotateTo(this.gameObject, iTween.Hash(
                "z", -tiltDigree,
                 "oncompletetarget", this.gameObject,
                "oncomplete", "ToFazeSTAGE_TILT"));
        }
        else if (direction == "l")
        {
            this.direction = 4;
            //それぞれのブロックの移動位置設定
            for (int horizon = 1; horizon < horizonSize; horizon++)
            {
                for (int vertical = 0; vertical < verticalSize; vertical++)
                {
                    if (isBlockExit[vertical, horizon] < 0)
                    {
                        continue;
                    }
                    var t = horizon - 1;
                    var number = isBlockExit[vertical, horizon];
                    while (0 <= t)
                    {
                        if (isBlockExit[vertical, t] != -1)
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
            iTween.RotateTo(this.gameObject, iTween.Hash(
                "z", tiltDigree,
                 "oncompletetarget", this.gameObject,
                "oncomplete", "ToFazeSTAGE_TILT"));
        }
    }

    public void GameReset()
    {
        charaController.charaExitBlockPosition = Vector3.zero;
        GameBack(0);
    }

    public void GameRetrun()
    {
        var lastIndex = returnGameClass.Count-1;
        if (0 < lastIndex)
        {
            GameBack(lastIndex);
            returnGameClass.RemoveAt(lastIndex);
        }
    }

    public void PrepareReturn()
    {
        var returnPosi = new Vector3[verticalSize * horizonSize];
        var retrunIsExi = new int[verticalSize, horizonSize];
        //リターン位置の登録
        for (var i = 0; i < verticalSize * horizonSize; i++)
        {
            if (gameBlockPosition[i] != null)
            {
                returnPosi[i] = gameBlockPosition[i].transform.localPosition;
            }
            else
            {
                returnPosi[i] = new Vector3(-1, -1, -1);
            }
            retrunIsExi[i / horizonSize, i % horizonSize] = isBlockExit[i / horizonSize, i % horizonSize];
        }
        var tempRetrunGmaeClass = new ReturnGameClass(returnPosi, charaController.GetReturnCharaPosition(), retrunIsExi);
        returnGameClass.Add(tempRetrunGmaeClass);
    }

    private void GameBack(int backIndex)
    {
        charahitToWall = false;
        isCharaHitToIn = true;
        stageFaze = StageFaze.IDLE;
        buttonController.ChangeInteractableToTrue();
        this.transform.rotation = Quaternion.Euler(0, this.transform.localRotation.eulerAngles.y, 0);
        var tempReturnGameClass = returnGameClass[backIndex];
        for (var i = 0; i < verticalSize * horizonSize; i++)
        {
            if (gameBlockPosition[i] != null)
            {
                gameBlockPosition[i].transform.localPosition = tempReturnGameClass.retrunGameBlockPosition[i];
                destination[i] = tempReturnGameClass.retrunGameBlockPosition[i];
            }
            isBlockExit[i / horizonSize, i % horizonSize] = tempReturnGameClass.returnIsExitBlock[i / horizonSize, i % horizonSize];
        }
        charaController.ReturnCharaPosition(tempReturnGameClass.returnCharaPosition);
    }

    private void ToFazeSTAGE_TILT()
    {
        stageFaze = StageFaze.BLOCK_MOVE;
        buttonController.ResetReturnToTure();
    }

    private void ToFazeCHARA_MOVE_DEFAULT()
    {
        stageFaze = StageFaze.CHARA_MOVE_DEFAULT;
        animator.SetTrigger("SetMove");
        if (isCharaHitToIn)
        {
            //初回のEnterが子リジョンしてない
            charaController.charaExitBlockPosition = gameBlockPosition[charaEnterBlock].transform.localPosition;
            var rig = chara.GetComponent<Rigidbody>();
            rig.drag = 1;
        }
        else
        {
            charaController.charaExitBlockPosition = gameBlockPosition[charaExitBlock].transform.localPosition;
        }
    }

    private bool DoseStop()
    {
        for (var index = 0; index < verticalSize * horizonSize; index++)
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

    public class ReturnGameClass
    {
        public Vector3[] retrunGameBlockPosition { get; set; }
        public Vector3 returnCharaPosition { get; set; }
        public int[,] returnIsExitBlock { get; set; }

        public ReturnGameClass(Vector3[] retrunPosi, Vector3 returnCharaPosi, int[,] isExblo)
        {
            this.retrunGameBlockPosition = retrunPosi;
            this.returnCharaPosition = returnCharaPosi;
            this.returnIsExitBlock = isExblo;
        }
    }
}