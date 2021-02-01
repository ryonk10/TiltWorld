using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    public float walkSpeed;
    public Vector3 charaExitBlockPosition { get; set; }
    public bool doseStageReturn { get; set; }
    public bool notGoal { get; set; }

    private ButtonController buttonController;
    private Vector3 charaDefaltPosition;
    private Rigidbody rigi;

    // Start is called before the first frame update
    private void Start()
    {
        buttonController = GameObject.FindGameObjectWithTag("ButtonController").GetComponent<ButtonController>();
        doseStageReturn = false;
        notGoal = true;
        rigi = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (doseStageReturn&&notGoal)
        {
            float step = walkSpeed * Time.deltaTime;
            charaDefaltPosition = new Vector3(charaExitBlockPosition.x, this.transform.localPosition.y, charaExitBlockPosition.z);
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, charaDefaltPosition, step);
            if ((charaDefaltPosition.x==this.transform.localPosition.x) 
                && (charaDefaltPosition.z==this.transform.localPosition.z))
            {
                doseStageReturn = false;
               
                buttonController.ChangeInteractableToTrue();
            }
        }
        else if (notGoal == false)
        {
            float step = 5 * Time.deltaTime;
            this.rigi.velocity = new Vector3(1, 0, 5)*step;
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