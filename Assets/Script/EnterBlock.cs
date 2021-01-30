using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBlock : MonoBehaviour
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
            stageCon.charaEnterBlock = int.Parse(this.name);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "chara")
        {
            stageCon.charaExitBlock = int.Parse(this.name);
        }
    }
}
