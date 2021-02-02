using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditController : MonoBehaviour
{
    public GameObject canvas;
    // Start is called before the first frame update
    
    public void FinishEditMode()
    {
        var positionBlocks = GameObject.FindGameObjectsWithTag("PositionBlock");
        foreach (var block in positionBlocks)
        {
            Destroy(block);
        }
        var baseBlock = GameObject.FindGameObjectsWithTag("BaseEditBlock");
        foreach(var block in baseBlock)
        {
            Destroy(block);
        }
        var editBlock = GameObject.FindGameObjectsWithTag("EditBlock");
        foreach(var block in editBlock)
        {
            Destroy(block.GetComponent<EditBlock>());
            block.GetComponent<EnterBlock>().enabled = true;
            block.tag = "block";
        }
        Destroy(GameObject.FindGameObjectWithTag("Chara").GetComponent<EditChara>());
        canvas.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
