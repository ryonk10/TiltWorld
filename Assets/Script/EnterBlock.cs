using UnityEngine;
using System.Text.RegularExpressions;
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
            var numberStr = Regex.Match(this.name, @"^(\d+)");
            if (numberStr.Success)
            {
                stageCon.charaEnterBlock = int.Parse(numberStr.Value);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chara"))
        {
            var numberStr = Regex.Match(this.name, @"^(\d+)");
            if (numberStr.Success)
            {
                stageCon.charaExitBlock = int.Parse(numberStr.Value);
            }
        }
    }
}