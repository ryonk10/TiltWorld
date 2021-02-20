using System.Text.RegularExpressions;
using UnityEngine;

public class EnterBlock : MonoBehaviour
{
    private StageController stageController;
    // Start is called before the first frame update

    private void Start()
    {
        stageController = GameObject.FindGameObjectWithTag("Stage").GetComponent<StageController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chara"))
        {
            var numberStr = Regex.Match(this.name, @"^(\d+)");
            if (numberStr.Success)
            {
                stageController.charaEnterBlock = int.Parse(numberStr.Value);
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
                stageController.charaExitBlock = int.Parse(numberStr.Value);
            }
        }
    }
}