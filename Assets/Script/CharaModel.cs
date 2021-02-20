using System.Collections;
using UnityEngine;

public class CharaModel : MonoBehaviour
{
    public float walkSpeed;
    public Vector3 charaExitBlockPosition { get; set; }

    private Rigidbody rigi;

    private void Start()
    {
        rigi = this.GetComponent<Rigidbody>();
    }

    public IEnumerator MoveDefaulPosition()
    {
        Stage.stagePhase = StagePhase.CHARA_MOVING;
        var charaDefaltPosition = new Vector3(charaExitBlockPosition.x, this.transform.localPosition.y, charaExitBlockPosition.z);
        rigi.drag = 1;
        while ((charaDefaltPosition.x != this.transform.localPosition.x)
            && (charaDefaltPosition.z != this.transform.localPosition.z))
        {
            float step = walkSpeed * Time.deltaTime;
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, charaDefaltPosition, step);
            yield return null;
        }
        Stage.stagePhase = StagePhase.FINISH;
    }

    public IEnumerator GoalMove()
    {
        var time = 0f;
        while (time < 2)
        {
            time += Time.deltaTime;
            float step = 3 * Time.deltaTime;
            this.rigi.velocity = new Vector3(1, 0, 5) * step;
            yield return null;
        }
    }

    public void CharaInitialize()
    {
        rigi.useGravity = true;
        rigi.drag = 1;
    }

    public void ReturnCharaPosition(Vector3 charaPosi)
    {
        var block = GameObject.FindGameObjectWithTag("block");
        this.transform.parent = block.transform.parent;
        this.rigi.drag = 1;
        this.transform.localPosition = charaPosi;
    }
}