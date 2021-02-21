using UnityEngine;

public class CharaController : MonoBehaviour
{
    public CharaModel charaModel;

    public void CharaInitialize()
    {
        charaModel.CharaInitialize();
    }
    public void CharaMoveDefaultPosition()
    {
        StartCoroutine(charaModel.MoveDefaulPosition());
    }
    public void CharaOnGoal()
    {
        StartCoroutine(charaModel.GoalMove());
    }

    public void ChangeParent(Transform parent)
    {
        charaModel.transform.parent = parent;
    }

    public Vector3 GetCharaPosition()
    {
        return charaModel.transform.localPosition;
    }
    public void SetCharaExitBlockPosition(Vector3 position)
    {
        charaModel.charaExitBlockPosition = position;
    }
    public void ReturnCharaPosition(Vector3 charaPosition)
    {
        charaModel.ReturnCharaPosition(charaPosition);
    }
}