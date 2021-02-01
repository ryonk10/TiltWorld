using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditBlock : MonoBehaviour,IPointerClickHandler,IDragHandler,IBeginDragHandler,IEndDragHandler,IDropHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.transform.Rotate(0, 90, 0);
    }
    public void OnDrag(PointerEventData eventData)
    {
        var ray = RectTransformUtility.ScreenPointToRay(Camera.main, eventData.position);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            this.transform.position = hit.point;
        }
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

   
}
