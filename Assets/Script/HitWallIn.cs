using UnityEngine;

public class HitWallIn : MonoBehaviour
{
    private StageController stageController;

    // Start is called before the first frame update
    private void Start()
    {
        stageController = GameObject.FindGameObjectWithTag("Stage").GetComponent<StageController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chara"))
        {
            stageController.isCharaHitToIn = true;
            stageController.charahitToWall = true;
            if (this.gameObject.tag != "wall")
            {
                var rig = collision.gameObject.GetComponent<Rigidbody>();
                rig.drag = 50;
                collision.gameObject.transform.parent = this.transform.parent.parent.parent;
            }
        }
    }
}