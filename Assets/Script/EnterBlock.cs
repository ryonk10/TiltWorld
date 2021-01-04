using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBlock : MonoBehaviour
{
    public stageController stageCon;
    // Start is called before the first frame update
   

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
