using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController: MonoBehaviour
{
    public ButtonController buttonController;
    public GameObject CanvasTutorial;
    public Button rightButton;
    public Button resetButton;
    public Text textInformation;
    public GameObject tutorialSupportImage;
    public GameObject tutorialSupportImage1;
    public GameObject tutorialSupportImage2;
    public GameObject informationSupportImage;
    public GameObject checkTutorial;
    public GameObject block;
    public GameObject informationPanel;
    public GameObject GameInfoPanel;
    public Stage stage;
    public Rigidbody charaRigit;

    private int direction;
    private bool buttonflag;
    private bool resetflag;

    // Start is called before the first frame update
    private void Start()
    {
        buttonflag = false;
        resetflag = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if(buttonflag)
        buttonController.MoveButtonToFalse();
        if (resetflag)
            buttonController.ResetReturnToFalse();
    }

    private IEnumerator TutorialPhase()
    {
        informationPanel.SetActive(true);
        textInformation.text = "傾くセカイへようこそ！！\nこの世界では、キャラクターを\n動かすのではなく、世界を動か\nします。";
        informationSupportImage.SetActive(true);
        yield return null;
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        textInformation.text = "では、さっそく世界を\n傾けてみましょう";
        rightButton.interactable = true;
        tutorialSupportImage.SetActive(true);
        informationSupportImage.SetActive(false);
        yield return new WaitUntil(() => (0 < stage.tiltDirection));
        buttonflag = true;
        this.direction = stage.tiltDirection;
        tutorialSupportImage.SetActive(false);
        yield return new WaitForSeconds(2f);
        textInformation.text = "世界を傾けると、地面が移動し、\nキャラクターは世界の壁\nもしくは物にぶつかるまで\n滑ってしまいます。";
        stage.tiltDirection = 0;
        informationSupportImage.SetActive(true);
        charaRigit.isKinematic = true;
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        stage.tiltDirection = this.direction;
        charaRigit.isKinematic = false;
        yield return null;
        textInformation.text = "また、時に間違ってしまい\nやり直したいことも\nあるでしょう。";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        tutorialSupportImage1.SetActive(true);
        tutorialSupportImage2.SetActive(true);
        resetflag = false;
        textInformation.text = "大丈夫です。\nそういう方の為に\nリセットボタン・リターンボタン\nがあります。";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        textInformation.text = "リセットボタンは最初の位置に、\nリターンボタンは一つ前の\n位置に戻ります。";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        textInformation.text = "今回はリセットボタンを\n押しましょう。";
        tutorialSupportImage2.SetActive(false);
        informationSupportImage.SetActive(false);
        resetButton.interactable = true;
        yield return new WaitUntil(() => block.gameObject.transform.localPosition == Vector3.zero);
        yield return null;
        tutorialSupportImage1.SetActive(false);
        informationSupportImage.SetActive(true);
        resetButton.interactable = false;
        resetflag = true;
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
        StartCoroutine(TutorialPhase());
    }

    public void NoTutorial()
    {
        buttonController.ButtonToTure();
        GameInfoPanel.SetActive(true);
        Destroy(CanvasTutorial);
        Destroy(this.gameObject);
    }
}