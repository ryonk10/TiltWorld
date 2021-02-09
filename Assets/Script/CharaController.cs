﻿using UnityEngine;

public class CharaController : MonoBehaviour
{
    public float walkSpeed;
    public Vector3 charaExitBlockPosition { get; set; }

    private ButtonController buttonController;
    private stageController stageCon;
    private Vector3 charaDefaltPosition;
    private Rigidbody rigi;

    // Start is called before the first frame update
    private void Start()
    {
        buttonController = GameObject.FindGameObjectWithTag("ButtonController").GetComponent<ButtonController>();
        stageCon = GameObject.FindGameObjectWithTag("Stage").GetComponent<stageController>();
        rigi = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (stageCon.stageFaze == stageController.StageFaze.CHARA_MOVE_DEFAULT)
        {
            float step = walkSpeed * Time.deltaTime;
            charaDefaltPosition = new Vector3(charaExitBlockPosition.x, this.transform.localPosition.y, charaExitBlockPosition.z);
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, charaDefaltPosition, step);
            if ((charaDefaltPosition.x == this.transform.localPosition.x)
                && (charaDefaltPosition.z == this.transform.localPosition.z))
            {
                stageCon.stageFaze = stageController.StageFaze.IDLE;
                buttonController.ChangeInteractableToTrue();
            }
        }
        else if (stageCon.stageFaze == stageController.StageFaze.GOAL)
        {
            float step = 10 * Time.deltaTime;
            this.rigi.velocity = new Vector3(1, 0, 5) * step;
        }
    }

    public Vector3 GetReturnCharaPosition()
    {
        return this.transform.localPosition;
    }

    public void ReturnCharaPosition(Vector3 charaPosi)
    {
        var block = GameObject.FindGameObjectWithTag("block");
        this.transform.parent = block.transform.parent;
        this.rigi.drag = 1;
        this.transform.localPosition = charaPosi;
    }

    public void MoveParentToBlock(GameObject target)
    {
        this.transform.parent = target.transform.parent;
    }
}