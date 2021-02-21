using UnityEngine;

public class HitWallOut : MonoBehaviour
{
    private Stage stage;
    private void Start()
    {
        stage = GameObject.FindGameObjectWithTag("Stage").GetComponent<Stage>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chara"))
        {
            stage.isCharaHitToIn = false;
            stage.charahitToWall = true;
        }
    }
}