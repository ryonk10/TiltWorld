using System.Text.RegularExpressions;
using UnityEngine;

public class EnterBlock : MonoBehaviour
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
            var numberStr = Regex.Match(this.name, @"^(\d+)");
            if (numberStr.Success)
            {
                stage.charaEnterBlock=int.Parse(numberStr.Value);
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
                stage.charaExitBlock=int.Parse(numberStr.Value);
            }
        }
    }
}