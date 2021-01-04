using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWallIn : MonoBehaviour
{
    public stageController stageCon;

    // Start is called before the first frame update
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "chara")
        {
            stageCon.isCharaHitToIn = true;
            stageCon.charahitToWall = true;
        }
    }
}
