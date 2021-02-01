using UnityEngine;
using UnityEngine.EventSystems;

public class EditBlock : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    private GameObject blockH;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.transform.Rotate(0, 90, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
       blockH.transform.position= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        blockH = (GameObject)Instantiate(this.gameObject, this.transform.position, this.transform.rotation);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(blockH);
        Debug.Log("end");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("s");
    }
}