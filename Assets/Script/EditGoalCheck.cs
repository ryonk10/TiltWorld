using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditGoalCheck : MonoBehaviour
{
    public GameObject goalInfo;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chara"))
        {
            other.gameObject.GetComponent<CharaController>().notGoal = false;
            goalInfo.SetActive(true);
        }
    }
}
