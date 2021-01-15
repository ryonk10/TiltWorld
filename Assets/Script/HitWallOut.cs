﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWallOut : MonoBehaviour
{
    public stageController stageCon;

    // Start is called before the first frame update
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "chara")
        {
            stageCon.isCharaHitToIn = false;
            stageCon.charahitToWall = true;
            var rig = collision.gameObject.GetComponent<Rigidbody>();
            rig.drag = 50;
            collision.gameObject.transform.parent = this.transform.parent.parent.parent;
        }
    }
}
