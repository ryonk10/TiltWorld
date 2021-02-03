using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditController : MonoBehaviour
{
    public GameObject canvas;
    public GameObject errainfo;
    public GameObject chara;
    public GameObject[] baseBlocks;
    public Button playButton;
    public Button editButton;
    public Button ok;
    public Text erraText;
    public Vector3 editCharaPosi { get; set; }
    public Vector3 editCharaScale { get; set; }

    private stageController stageCon;

    private void Start()
    {
        editCharaPosi = chara.transform.position;
        editCharaScale = chara.transform.localScale;
        stageCon = GameObject.FindGameObjectWithTag("Stage").GetComponent<stageController>();
    }

    private void Update()
    {
        if (stageCon.blockMoveFlag)
        {
            playButton.interactable = false;
            editButton.interactable = false;
        }
        else
        {
            if (stageCon.isEditMode)
            {
                playButton.interactable = true;
                editButton.interactable = false;
            }
            else
            {
                playButton.interactable = false;
                editButton.interactable = true;
            }
        }
    }
    public void FinishEditMode()
    {
        var editBlock = GameObject.FindGameObjectsWithTag("EditBlock");
        if (editBlock.Length == 0)
        {
            ErraShow("バグるとでも思ったか！！");
            return;
        }
        if (!IsCharaOnBlock())
        {
            ErraShow("なめとんのか！！！");
            return;
        }
       
        var positionBlocks = GameObject.FindGameObjectsWithTag("PositionBlock");
        foreach (var block in positionBlocks)
        {
            Destroy(block);
        }
        foreach(var block in baseBlocks)
        {
            block.SetActive(false);
        }
        foreach(var block in editBlock)
        {
            Destroy(block.GetComponent<EditBlock>());
            block.GetComponent<EnterBlock>().enabled = true;
            block.tag = "block";
        }
        Destroy(chara.GetComponent<EditChara>());
        canvas.SetActive(true);
        playButton.interactable = false;
        editButton.interactable = true;
        stageCon.StageInitialize();
    }

    public void ReStartEditMode()
    {
        playButton.interactable = true;
        editButton.interactable = false;
        canvas.SetActive(false);
        chara.GetComponent<Rigidbody>().useGravity = false;
        var editChara=chara.AddComponent<EditChara>();
        chara.transform.parent = null;
        chara.transform.position = editCharaPosi;
        chara.transform.localScale = editCharaScale;
        chara.tag = "EditChara";
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
        erraText.text=messe;
        errainfo.SetActive(true);
    }

    public void OK()
    {
        errainfo.SetActive(false);
    }
}
