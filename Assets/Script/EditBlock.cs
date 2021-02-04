using UnityEngine;
using UnityEngine.EventSystems;

public class EditBlock : MonoBehaviour, IInitializePotentialDragHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private GameObject block;
    private bool DoseMoveEditBlock = false;
    private bool isHitEditBlock = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!DoseMoveEditBlock)
        {
            this.transform.Rotate(0, 90, 0);
        }
        DoseMoveEditBlock = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        block.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, Camera.main.transform.position.y - 3));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DoseMoveEditBlock = true;
        if (isHitEditBlock)
        {
            block = this.gameObject;
            block.transform.parent = null;
        }
        else
        {
            block = (GameObject)Instantiate(this.gameObject, this.transform.position, this.transform.rotation);
        }
        block.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        block.tag = "Untagged";
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isHitEditBlock = false;
        var isHitPositionBlock = false;
        var ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        foreach (var hit in Physics.RaycastAll(ray))
        {
            var hitBlock = hit.collider.gameObject;
            if (hitBlock.CompareTag("EditBlock"))
            {
                Destroy(hitBlock);
            }
            else if (hitBlock.CompareTag("PositionBlock"))
            {
                block.transform.parent = hitBlock.transform.parent;
                var position = new Vector3(hitBlock.transform.localPosition.x, 0, hitBlock.transform.localPosition.z);
                block.transform.localPosition = position;
                block.transform.localScale = new Vector3(1, 1, 1);
                isHitPositionBlock = true;
            }
        }
        if (isHitPositionBlock)
        {
            block.tag = "EditBlock";
        }
        else
        {
            Destroy(block);
        }
        block = null;
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        var ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        foreach (var hit in Physics.RaycastAll(ray))
        {
            var hitBlock = hit.collider.gameObject;
            if (hitBlock.CompareTag("EditBlock"))
            {
                isHitEditBlock = true;
                break;
            }
        }
    }
}