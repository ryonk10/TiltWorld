using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEvent: MonoBehaviour
{
    public Button up;
    public Button down;
    public Button right;
    public Button left;
    public Button resetbut;
    public Button returnbut;
    public Text textInformation;
    public GameObject tutorialSupportImage;
    public GameObject informationSupportImage;
    public stageController stageCon;
    public Rigidbody charaRigit;

    private int direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(TutorialFaze());
        }
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                StartCoroutine(TutorialFaze());
            }
        }
    }

    IEnumerator TutorialFaze()
    {
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        textInformation.text = "世界を傾けてみましょう";
        right.interactable = true;
        tutorialSupportImage.SetActive(true);
        yield return new WaitUntil(() =>(0<stageCon.direction));
        textInformation.text = null;
        informationSupportImage.SetActive(false);
        this.direction = stageCon.direction;
        tutorialSupportImage.SetActive(false);
        yield return new WaitForSeconds(2f);
        textInformation.text = "世界を傾けると、地面が移動し、\nキャラクターは世界の壁\nもしくは物にぶつかるまで\n滑ってしまいます。";
        stageCon.direction = 0;
        informationSupportImage.SetActive(true);
        charaRigit.isKinematic = true;
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        stageCon.direction = this.direction;
        charaRigit.isKinematic = false;
        textInformation.text = "";
        yield break;
    }
}
