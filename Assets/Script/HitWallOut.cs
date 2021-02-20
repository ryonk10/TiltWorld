using UnityEngine;

public class HitWallOut : MonoBehaviour
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
            stageController.isCharaHitToIn = false;
            stageController.charahitToWall = true;
        }
    }
}