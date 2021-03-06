﻿using UnityEngine;

public class HitWallIn : MonoBehaviour
{
    private Stage stage;

    // Start is called before the first frame update
    private void Start()
    {
        stage = GameObject.FindGameObjectWithTag("Stage").GetComponent<Stage>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chara"))
        {
            stage.isCharaHitToIn = true;
            stage.charahitToWall = true;
            if (this.gameObject.tag != "wall")
            {
                var rig = collision.gameObject.GetComponent<Rigidbody>();
                rig.drag = 50;
                collision.gameObject.transform.parent = this.transform.parent.parent.parent;
            }
        }
    }
}