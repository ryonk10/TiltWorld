using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    public ButtonController buttonController;
    public float walkSpeed;
    public Vector3 charaExitBlockPosition { get; set; }
    public bool doseStageReturn { get; set; }

    private Vector3 charaDefaltPosition;

    // Start is called before the first frame update
    private void Start()
    {
        doseStageReturn = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (doseStageReturn)
        {
            float step = walkSpeed * Time.deltaTime;
            charaDefaltPosition = new Vector3(charaExitBlockPosition.x, 0.5f, charaExitBlockPosition.z);
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, charaDefaltPosition, step);
            if ((charaDefaltPosition.x - 0.1 < this.transform.localPosition.x) && (this.transform.localPosition.x < charaDefaltPosition.x + 0.1) &&
                (charaDefaltPosition.z - 0.1 < this.transform.localPosition.z) && (this.transform.localPosition
                .z < charaDefaltPosition.z + 0.1))
            {
                doseStageReturn = false;
                buttonController.ChangeInteractableToTrue();
            }
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