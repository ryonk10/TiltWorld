using System.Collections;
using UnityEngine;

public class EndEvetController : MonoBehaviour
{
    public GameInfoController gameInfoController;
    public Animator clearSphareAnim;
    public Camera subCamera;
    public GameObject wall;
    public GameObject textGoalInfo;
    public GameObject clearForm;
    public GameObject buttonPanel;
    public GameObject gameInfoPanel;
    public void OnGoal()
    {
        Stage.stagePhase = StagePhase.GOAL;
        Camera.main.enabled = false;
        subCamera.enabled = true;
        textGoalInfo.SetActive(true);
        clearForm.SetActive(true);
        buttonPanel.SetActive(false);
        gameInfoPanel.SetActive(false);
        wall.SetActive(false);
        gameInfoController.SetTextEndIfo("次のステージへ行きますか？");
        clearSphareAnim.SetTrigger("StartCutOut");
        StartCoroutine(ShowEndCanvas());
    }

    private IEnumerator ShowEndCanvas()
    {
        yield return null;
        while (!clearSphareAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            yield return null;
        }
        gameInfoController.ShowEndForm();
    }
}