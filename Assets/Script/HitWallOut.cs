using System.Collections;
using System.Collections.Generic;
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
        if (collision.gameObject.name == "chara")
        {
            stageCon.isCharaHitToIn = false;
            stageCon.charahitToWall = true;
        }
    }
}
