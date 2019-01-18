using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    #region Variable Declarations
    public ItemConfig itemConfig;
    public Image image;
    public Text amountText;
    public int ItemAmount;
    public bool isEmpty;
    #endregion
    private void Start()
    {
        UpdateUI();

    }
    public void UpdateUI()
    {
        image.sprite = itemConfig.itemIcon;
        if(ItemAmount<1)
            amountText.text = "";
        else
            amountText.text = "" + ItemAmount;
    }

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invSlot = transform as RectTransform;
        if (RectTransformUtility.RectangleContainsScreenPoint(invSlot, Input.mousePosition))
        {
            //ItemConfig tempItem = itemConfig;
            itemConfig = eventData.pointerDrag.GetComponent<InventorySlot>().itemConfig;
            UpdateUI();
            Debug.Log("Swaped Item");
        }
    }
}