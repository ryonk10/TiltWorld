using UnityEngine;
using UnityEngine.Events;

public class GoalCheker : MonoBehaviour
{
    public UnityEvent OnGoal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chara"))
        {
            OnGoal.Invoke();
        }
    }
}