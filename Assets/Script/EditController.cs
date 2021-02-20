using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EditController : MonoBehaviour
{
    public GameObject canvas;
    public GameObject errainfo;
    public GameObject goalInfo;
    public GameObject OpenLoadForm;
    public GameObject OpenSaveForm;
    public GameObject chara;
    public GameObject goal;
    public GameObject[] baseBlocks;
    public Button playButton;
    public Button editButton;
    public Button ok;
    public Text erraText;
    public Text goalText;
    public Text moveCountText;
    public Vector3 editCharaPosi { get; set; }
    public Vector3 editCharaScale { get; set; }
    public Vector3 editGoalPosi { get; set; }
    public Vector3 editGoalScale { get; set; }

    private StageController stageController;
    private int saveNumber;
    private SaveDataClass saveDataClass;

    private void Start()
    {
        editCharaPosi = chara.transform.position;
        editCharaScale = chara.transform.localScale;
        editGoalPosi = goal.transform.position;
        editGoalScale = goal.transform.localScale;
        stageController = GameObject.FindGameObjectWithTag("Stage").GetComponent<StageController>();
    }

    private void Update()
    {
        if (Stage.stagePhase == StagePhase.IDLE)
        {
            if (stageController.GetIsEditMode())
            {
                playButton.interactable = true;
                editButton.interactable = false;
                OpenLoadForm.SetActive(true);
                OpenSaveForm.SetActive(true);
            }
            else
            {
                playButton.interactable = false;
                editButton.interactable = true;
                OpenLoadForm.SetActive(false);
                OpenSaveForm.SetActive(false);
            }
        }
        else
        {
            playButton.interactable = false;
            editButton.interactable = false;
        }
    }

    public void FinishEditMode()
    {
        var editBlock = GameObject.FindGameObjectsWithTag("EditBlock");
        if (editBlock.Length == 0)
        {
            ErraShow("ブロックを配置してください。");
            return;
        }
        if (!IsCharaOnBlock())
        {
            ErraShow("キャラを配置してください。");
            return;
        }

        var positionBlocks = GameObject.FindGameObjectsWithTag("PositionBlock");
        foreach (var block in positionBlocks)
        {
            Destroy(block);
        }
        var positionGoals = GameObject.FindGameObjectsWithTag("PositionGoal");
        foreach (var block in positionGoals)
        {
            Destroy(block);
        }
        foreach (var block in baseBlocks)
        {
            block.SetActive(false);
        }
        foreach (var block in editBlock)
        {
            Destroy(block.GetComponent<EditBlock>());
            block.GetComponent<EnterBlock>().enabled = true;
            block.tag = "block";
        }
        Destroy(chara.GetComponent<EditChara>());
        chara.tag = "Chara";
        Destroy(goal.GetComponent<EditGoal>());
        canvas.SetActive(true);
        playButton.interactable = false;
        editButton.interactable = true;
        moveCountText.text = "0";
        stageController.SetIsEditMode(false);
        Stage.stagePhase = StagePhase.INITIALIZE;
    }

    public void ReStartEditMode()
    {
        playButton.interactable = true;
        editButton.interactable = false;
        goal.SetActive(true);
        canvas.SetActive(false);
        chara.GetComponent<Rigidbody>().useGravity = false;
        chara.AddComponent<EditChara>();
        chara.transform.parent = null;
        chara.transform.position = editCharaPosi;
        chara.transform.localScale = editCharaScale;
        chara.tag = "EditChara";
        goal.AddComponent<EditGoal>();
        goal.transform.parent = null;
        goal.transform.position = editGoalPosi;
        goal.transform.localScale = editGoalScale;
        foreach (var block in baseBlocks)
        {
            block.SetActive(true);
        }
        var gameBlock = GameObject.FindGameObjectsWithTag("block");
        foreach (var block in gameBlock)
        {
            block.AddComponent<EditBlock>();
            block.GetComponent<EnterBlock>().enabled = false;
            block.tag = "EditBlock";
        }
        GameObject.FindGameObjectWithTag("Stage").GetComponent<Animator>().SetTrigger("SetIdle");
        stageController.SetIsEditMode(true);
    }

    private bool IsCharaOnBlock()
    {
        var ray = new Ray(chara.transform.position, new Vector3(0, -1, 0));
        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.CompareTag("EditBlock"))
            {
                return true;
            }
        }
        return false;
    }

    public void ErraShow(string messe)
    {
        playButton.image.color = playButton.colors.normalColor;
        erraText.text = messe;
        errainfo.SetActive(true);
    }

    public void ErraSorry()
    {
        errainfo.SetActive(false);
    }

    public void GoalOK()
    {
        goalInfo.SetActive(false);
    }

    public void OnGoal()
    {
        goalText.text = "移動回数" + moveCountText.text + "回でクリア！！";
        goalInfo.SetActive(true);
    }

    public void Save()
    {
        var blocks = GameObject.FindGameObjectsWithTag("EditBlock");
        var blockTransforms = new BlockTransform[blocks.Length];
        for (var index = 0; index < blocks.Length; index++)
        {
            var blockTransform = new BlockTransform();
            blockTransform.blockPosition = blocks[index].transform.localPosition;
            blockTransform.blockRotation = blocks[index].transform.localEulerAngles;
            blockTransform.blockType = blocks[index].name;
            blockTransforms[index] = blockTransform;
        }
        var stagePlacement = new StagePlacement
        {
            blockTransform = blockTransforms,
            charaPosition = chara.transform.localPosition,
            goalPosiotion = goal.transform.localPosition
        };
        Load();
        saveDataClass.saveData[saveNumber] = stagePlacement;
        var jsonString = JsonUtility.ToJson(saveDataClass);
        using (var file = new FileStream(Application.persistentDataPath + "/save.json", FileMode.Create, FileAccess.Write))
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);
            file.Write(bytes, 0, bytes.Length);
        }
        saveDataClass = null;
    }

    public void ReleseLoad()
    {
        saveDataClass = null;
    }

    public void Load()
    {
        saveDataClass = new SaveDataClass();
        if (!File.Exists(Application.persistentDataPath + "/save.json"))
        {
            var blockTransform = new BlockTransform[11]
            {
                new BlockTransform
                {
                    blockPosition=new Vector3(0,0,0),
                    blockRotation=Vector3.zero,
                    blockType="L"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(1,0,0),
                    blockRotation=Vector3.zero,
                    blockType="F"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(3,0,0),
                    blockRotation=Vector3.zero,
                    blockType="F"
                },
                 new BlockTransform
                {
                    blockPosition=new Vector3(0,0,1),
                    blockRotation=Vector3.zero,
                    blockType="Obs"
                },
                  new BlockTransform
                {
                    blockPosition=new Vector3(1,0,1),
                    blockRotation=Vector3.zero,
                    blockType="H"
                },
                   new BlockTransform
                {
                    blockPosition=new Vector3(2,0,1),
                    blockRotation=Vector3.zero,
                    blockType="Obs"
                },
                    new BlockTransform
                {
                    blockPosition=new Vector3(0,0,2),
                    blockRotation=Vector3.zero,
                    blockType="F"
                },
                 new BlockTransform
                {
                    blockPosition=new Vector3(2,0,2),
                    blockRotation=Vector3.zero,
                    blockType="F"
                },
                  new BlockTransform
                {
                    blockPosition=new Vector3(0,0,3),
                    blockRotation=Vector3.zero,
                    blockType="F"
                },
                   new BlockTransform
                {
                    blockPosition=new Vector3(2,0,3),
                    blockRotation=Vector3.zero,
                    blockType="F"
                },
                    new BlockTransform
                {
                    blockPosition=new Vector3(3,0,3),
                    blockRotation=new Vector3(0,90,0),
                    blockType="H"
                },
            };
            var stagePlacement = new StagePlacement[6];
            stagePlacement[0] = new StagePlacement
            {
                blockTransform = blockTransform,
                charaPosition = new Vector3(3, 0.75f, 3),
                goalPosiotion = new Vector3(0, 0, 3)
            };
            blockTransform = new BlockTransform[14]
            {
                new BlockTransform
                {
                    blockPosition=new Vector3(0,0,0),
                    blockRotation=new Vector3(0,90,0),
                    blockType="L"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(1,0,0),
                    blockRotation=Vector3.zero,
                    blockType="H"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(2,0,0),
                    blockRotation=Vector3.zero,
                    blockType="H"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(3,0,0),
                    blockRotation=Vector3.zero,
                    blockType="L"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(0,0,1),
                    blockRotation=new Vector3(0,90,0),
                    blockType="H"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(2,0,1),
                    blockRotation=Vector3.zero,
                    blockType="F"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(3,0,1),
                    blockRotation=new Vector3(0,90,0),
                    blockType="H"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(0,0,2),
                    blockRotation=new Vector3(0,90,0),
                    blockType="H"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(1,0,2),
                    blockRotation=Vector3.zero,
                    blockType="F"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(3,0,2),
                    blockRotation=new Vector3(0,90,0),
                    blockType="H"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(0,0,3),
                    blockRotation=new Vector3(0,180,0),
                    blockType="L"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(1,0,3),
                    blockRotation=Vector3.zero,
                    blockType="H"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(2,0,3),
                    blockRotation=Vector3.zero,
                    blockType="H"
                },
                new BlockTransform
                {
                    blockPosition=new Vector3(3,0,3),
                    blockRotation=new Vector3(0,-90,0),
                    blockType="L"
                }
            };
            stagePlacement[1] = new StagePlacement
            {
                blockTransform = blockTransform,
                charaPosition = new Vector3(0, 0.75f, 0),
                goalPosiotion = new Vector3(2, 0, 3)
            };
            saveDataClass.saveData = stagePlacement;
            return;
        }
        using (var file = new FileStream(Application.persistentDataPath + "/save.json", FileMode.Open, FileAccess.Read))
        {
            byte[] bytes = new byte[file.Length];
            file.Read(bytes, 0, bytes.Length);
            var jsonString = System.Text.Encoding.UTF8.GetString(bytes);
            saveDataClass = JsonUtility.FromJson<SaveDataClass>(jsonString);
        }
    }

    public void SetLoadPlacement(int number)
    {
        var blocks = GameObject.FindGameObjectsWithTag("EditBlock");
        var parent = GameObject.FindGameObjectWithTag("Stage").transform.Find("block").transform;
        foreach (var block in blocks)
        {
            Destroy(block);
        }
        var stagePlacement = saveDataClass.saveData[number];
        if (stagePlacement == null)
        {
            return;
        }
        for (var index = 0; index < stagePlacement.blockTransform.Length; index++)
        {
            GameObject tempBlock;
            if (stagePlacement.blockTransform[index].blockType.Contains("H"))
                tempBlock = baseBlocks[0];
            else if (stagePlacement.blockTransform[index].blockType.Contains("L"))
                tempBlock = baseBlocks[1];
            else if (stagePlacement.blockTransform[index].blockType.Contains("N"))
                tempBlock = baseBlocks[2];
            else if (stagePlacement.blockTransform[index].blockType.Contains("Obs"))
                tempBlock = baseBlocks[3];
            else
                tempBlock = baseBlocks[4];
            var block = Instantiate(tempBlock, parent);
            block.transform.localPosition = stagePlacement.blockTransform[index].blockPosition;
            block.transform.localEulerAngles = stagePlacement.blockTransform[index].blockRotation;
            block.transform.localScale = new Vector3(1, 1, 1);
            block.tag = "EditBlock";
        }
        if (stagePlacement.charaPosition != Vector3.zero && stagePlacement.charaPosition != editCharaPosi)
        {
            chara.transform.parent = parent;
            chara.transform.localPosition = stagePlacement.charaPosition;
            chara.transform.localScale = new Vector3(0.002f, 0.002f, 0.002f);
        }
        if (stagePlacement.goalPosiotion != Vector3.zero && stagePlacement.goalPosiotion != editGoalPosi)
        {
            parent = GameObject.FindGameObjectWithTag("PositionGoal").transform.parent;
            goal.transform.parent = parent;
            goal.transform.localPosition = stagePlacement.goalPosiotion;
            goal.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SelsectSave(int number)
    {
        saveNumber = number;
    }

    [Serializable]
    public class BlockTransform
    {
        public Vector3 blockPosition;
        public Vector3 blockRotation;
        public string blockType;
    }

    [Serializable]
    public class StagePlacement
    {
        public BlockTransform[] blockTransform;
        public Vector3 charaPosition;
        public Vector3 goalPosiotion;
    }

    [Serializable]
    public class SaveDataClass
    {
        public StagePlacement[] saveData;
    }
}