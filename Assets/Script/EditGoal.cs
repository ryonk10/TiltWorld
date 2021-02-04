using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditGoal : MonoBehaviour,IBeginDragHandler, IDragHandler,IEndDragHandler
{
    private EditController editController;
    private void Start()
    {
        editController = GameObject.FindGameObjectWithTag("EditController").GetComponent<EditController>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.parent = null;
        this.transform.localScale = editController.editGoalScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, Camera.main.transform.position.y));
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        var isHitPositionGoal = false;
        var ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        foreach (var hit in Physics.RaycastAll(ray))
        {
            var hitBlock = hit.collider.gameObject;
            if (hitBlock.CompareTag("PositionGoal"))
            {
                this.transform.parent = hitBlock.transform.parent;
                var position = new Vector3(hitBlock.transform.localPosition.x, 0, hitBlock.transform.localPosition.z);
                this.transform.localPosition = position;
                this.transform.localScale = new Vector3(1,1,1);
                isHitPositionGoal = true;
            }
        }
        if (!isHitPositionGoal)
        {
            this.transform.position = editController.editGoalPosi;
        }
    }
}