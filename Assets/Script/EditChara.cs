using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditChara : MonoBehaviour,IDragHandler,IEndDragHandler
{
    private Vector3 defaultPosi;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosi = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y - 3));
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        var isHitPositionBlock = false;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        foreach (var hit in Physics.RaycastAll(ray))
        {
            var hitBlock = hit.collider.gameObject;
            if (hitBlock.CompareTag("PositionBlock"))
            {
                this.transform.parent = hitBlock.transform.parent;
                var position = new Vector3(hitBlock.transform.localPosition.x, 0.72f, hitBlock.transform.localPosition.z);
                this.transform.localPosition = position;
                this.transform.localScale = new Vector3(0.002f, 0.002f, 0.002f);
                var rig = this.GetComponent<Rigidbody>();
                rig.useGravity = true;
                rig.drag = 1;
                isHitPositionBlock = true;
            }
        }
        if (!isHitPositionBlock)
        {
            this.transform.position = defaultPosi;
        }
    }
}
