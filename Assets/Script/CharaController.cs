using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    public ButtonController buttonController;
    public stageController stageCon;
    public float walkSpeed;
    public Vector3 charaExitBlockPosition { get; set; }
    public bool doseStageReturn { get; set; }
    public bool notGoal { get; set; }

    private Vector3 charaDefaltPosition;
    private Rigidbody rigi;

    // Start is called before the first frame update
    private void Start()
    {
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
            charaDefaltPosition = new Vector3(charaExitBlockPosition.x, 0.5f, charaExitBlockPosition.z);
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
            float step = 0.01f * Time.deltaTime;
            this.rigi.velocity = new Vector3(0, 0, 0.5f);
        }
    }

    public Vector3 GetReturnCharaPosition()
    {
        return this.transform.localPosition;
    }

    public void ReturnCharaPosition(Vector3 charaPosi)
    {
        this.transform.localPosition = charaPosi;
    }
}