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
            if (int.TryParse(this.name, out var number))
            {
                stageCon.charaEnterBlock = number;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "chara")
        {
            if (int.TryParse(this.name, out var number))
            {
                stageCon.charaExitBlock = number;
            }
        }
    }
}