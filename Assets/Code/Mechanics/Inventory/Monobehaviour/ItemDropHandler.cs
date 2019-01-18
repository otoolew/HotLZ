using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler {


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invSlot = transform as RectTransform;
        if (RectTransformUtility.RectangleContainsScreenPoint(invSlot, Input.mousePosition))
        {

            Debug.Log("Drop Item");
        }
        else
        {

        }
    }
}
