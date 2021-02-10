using UnityEngine;

public class HitWallIn : MonoBehaviour
{
    private stageController stageCon;

    // Start is called before the first frame update
    private void Start()
    {
        stageCon = GameObject.FindGameObjectWithTag("Stage").GetComponent<stageController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chara"))
        {
            stageCon.isCharaHitToIn = true;
            stageCon.charahitToWall = true;
            if (this.gameObject.tag != "wall")
            {
                var rig = collision.gameObject.GetComponent<Rigidbody>();
                rig.drag = 50;
                collision.gameObject.transform.parent = this.transform.parent.parent.parent;
            }
        }
    }
}