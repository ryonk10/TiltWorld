using UnityEngine;
using System.Linq;

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
        if (collision.gameObject.CompareTag("Chara"))
        {
            var numberStr = this.name.Replace('F', ' ');
            if (int.TryParse(numberStr, out var number))
            {
                stageCon.charaEnterBlock = number;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chara"))
        {
            var numberStr = this.name.Replace('F', ' ');
            if (int.TryParse(numberStr, out var number))
            {
                stageCon.charaExitBlock = number;
            }
        }
    }
}