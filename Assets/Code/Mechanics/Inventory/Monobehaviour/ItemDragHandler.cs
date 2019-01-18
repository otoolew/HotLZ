using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originParent = null;
    public Image itemImage;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //this.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemImage.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemImage.transform.localPosition = Vector3.zero;
    }
}
