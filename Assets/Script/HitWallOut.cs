using UnityEngine;

public class HitWallOut : MonoBehaviour
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
            stageCon.isCharaHitToIn = false;
            stageCon.charahitToWall = true;
        }
    }
}