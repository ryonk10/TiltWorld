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
    public GameObject tutorialSupportImage1;
    public GameObject tutorialSupportImage2;
    public GameObject informationSupportImage;
    public GameObject checkTutorial;
    public GameObject block;
    public GameObject informationPanel;
    public GameObject gameInfo;
    public stageController stageCon;
    public Rigidbody charaRigit;

    private int direction;
    private bool buttonflag;
    // Start is called before the first frame update
    void Start()
    {
        buttonflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonflag)
        {
            up.interactable =false;
            down.interactable = false;
            right.interactable = false;
            left.interactable = false;
        }
    }

    IEnumerator TutorialFaze()
    {
        informationPanel.SetActive(true);
        textInformation.text = "傾くセカイへようこそ！！\nこの世界では、キャラクターを\n動かすのではなく、世界を動か\nします。";
        informationSupportImage.SetActive(true);
        yield return null;
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        textInformation.text = "では、さっそく世界を\n傾けてみましょう";
        right.interactable = true;
        tutorialSupportImage.SetActive(true);
        informationSupportImage.SetActive(false);
        yield return new WaitUntil(() =>(0<stageCon.direction));
        this.direction = stageCon.direction;
        tutorialSupportImage.SetActive(false);
        yield return new WaitForSeconds(2f);
        textInformation.text = "世界を傾けると、地面が移動し、\nキャラクターは世界の壁\nもしくは物にぶつかるまで\n滑ってしまいます。";
        stageCon.direction = 0;
        informationSupportImage.SetActive(true);
        buttonflag = true;
        charaRigit.isKinematic = true;
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        stageCon.direction = this.direction;
        charaRigit.isKinematic = false;
        yield return null;
        textInformation.text = "また、時に間違ってしまい\nやり直したいことも\nあるでしょう。";
        yield return new WaitUntil(() =>Input.GetMouseButtonUp(0));
        yield return null;
        tutorialSupportImage1.SetActive(true);
        tutorialSupportImage2.SetActive(true);
        textInformation.text = "大丈夫です。\nそういう方の為に\nリセットボタン・リターンボタン\nがあります。";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        textInformation.text = "リセットボタンは最初の位置に、\nリターンボタンは一つ前の\n位置に戻ります。";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        textInformation.text = "今回はリセットボタンを\n押しましょう。";
        tutorialSupportImage2.SetActive(false);
        informationSupportImage.SetActive(false);
        resetbut.interactable = true;
        yield return new WaitUntil(() => block.gameObject.transform.localPosition==Vector3.zero);
        yield return null;
        tutorialSupportImage1.SetActive(false);
        informationSupportImage.SetActive(true);
        resetbut.interactable = false;
        textInformation.text = "これで最初の位置に\n戻りましたね。";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        textInformation.text = "さて、これにてチュートリアルは\nおしまいです。";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        textInformation.text = "それでは、世界を傾けて\n少女を導き、\n世界によって左右される\n少女の物語を最後まで\nお楽しみください。";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        NoTutorial();

    }

    public void YesTutorial()
    {
        Destroy(checkTutorial);
        StartCoroutine(TutorialFaze());
    }

    public void NoTutorial()
    {
        up.interactable = true;
        down.interactable = true;
        right.interactable = true;
        left.interactable = true;
        resetbut.interactable = true;
        returnbut.interactable = true;
        gameInfo.SetActive(true);
        Destroy(this.gameObject);
    }
}
