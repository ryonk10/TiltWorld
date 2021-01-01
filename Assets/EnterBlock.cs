using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBlock : MonoBehaviour
{
    public stageController stageCon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "chara")
        {
            stageCon.charaExitBlock = int.Parse(this.name);
        }
    }
}
