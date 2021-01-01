using UnityEngine;

public class CharaController : MonoBehaviour
{
    public ButtonController buttonController;
    public float walkSpeed;
    public Vector3 charaExitBlockPosition { get; set; }
    public bool doseStageReturn { get; set; } = false;

    private Vector3 charaDefaltPosition;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (doseStageReturn)
        {
            float step = walkSpeed * Time.deltaTime;
            charaDefaltPosition = new Vector3(charaExitBlockPosition.x, this.transform.position.y, charaExitBlockPosition.z);
            this.transform.position = Vector3.MoveTowards(this.transform.position, charaDefaltPosition, step);
            if ((charaDefaltPosition.x - 0.1 < this.transform.position.x) && (this.transform.position.x < charaDefaltPosition.x + 0.1) &&
                (charaDefaltPosition.z - 0.1 < this.transform.position.z) && (this.transform.position.z < charaDefaltPosition.z + 0.1))
            {
                doseStageReturn = false;
                buttonController.ChangeInteractableToTrue();
            }
        }
    }
}