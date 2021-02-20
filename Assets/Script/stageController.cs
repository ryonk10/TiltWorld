using UnityEngine;

public class StageController : MonoBehaviour
{
    public Stage stage;
    public ButtonController buttonController;
    public CharaController charaController;
    private void Update()
    {
        switch (Stage.stagePhase)
        {
            case StagePhase.INITIALIZE:
                StageInitializPhase();
                break;
            case StagePhase.IDLE:
                break;
            case StagePhase.STAGE_TILT:
                StageTiltPhase();
                break;
            case StagePhase.STAGE_TILTING:
                break;
            case StagePhase.BLOCK_SLIDE:
                SlidBlockPhase();
                break;
            case StagePhase.BLOCK_SLIDING:
                break;
            case StagePhase.STAGE_RETURN:
                StageRetrunPhase();
                break;
            case StagePhase.STAGE_RETURNING:
                break;
            case StagePhase.CHARA_MOVE_DEFAULT:
                CharaMoveDefaultPhase();
                break;
            case StagePhase.CHARA_MOVING:
                break;
            case StagePhase.FINISH:
                StageFinishPhase();
                break;
            case StagePhase.GOAL:
                GoalPhase();
                break;
                
        }
    }

    public void StageInitializPhase()
    {
        stage.StageInitialize();
        charaController.CharaInitialize();
    }

    public void StageTiltPhase()
    {
        stage.StageTilt();
    }

    public void SlidBlockPhase()
    {
        buttonController.ResetReturnToTure();
        StartCoroutine(stage.SlideBlock());
    }

    public void StageRetrunPhase()
    {
        charaController.ChangeParent(stage.transform.Find("block"));
        stage.StageRetrun();
    }
    public void GoalPhase()
    {
        charaController.CharaOnGoal();
    }
    public void CharaMoveDefaultPhase()
    {
        charaController.SetCharaExitBlockPosition(stage.GetCharaDefaultPosition());
        charaController.CharaMoveDefaultPosition();
    }

    public void StageFinishPhase()
    {
        buttonController.MoveButtonToTrue();
        Stage.stagePhase = StagePhase.IDLE;
    }
    public void SetTiltDirection(int tiltDirection)
    {
        stage.tiltDirection = tiltDirection;
        Stage.stagePhase = StagePhase.STAGE_TILT;
    }

    public void GameReset()
    {
        charaController.SetCharaExitBlockPosition(Vector3.zero);
        buttonController.MoveButtonToTrue();
        stage.GameReset();
    }
    public void GameRetrun()
    {
        buttonController.MoveButtonToTrue();
        stage.GameRetrun();
    }
    public void ReturnCharaPosition(Vector3 position)
    {
        charaController.ReturnCharaPosition(position);
    }

    public Vector3 GetCharaPosition()
    {
        return charaController.GetCharaPosition();
    }
    public bool GetIsEditMode()
    {
        return stage.isEditMode;
    }
    public void SetIsEditMode(bool isEditMode)
    {
        stage.isEditMode = isEditMode;
    }
}