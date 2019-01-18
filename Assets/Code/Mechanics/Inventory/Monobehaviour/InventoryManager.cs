using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager> 
{
    #region Variable Declarations
    public InventorySlot[] inventorySlots;

    #endregion
    #region Events
    public 

    #endregion
    #region Handlers


    #endregion
    #region Initializations
    // Use this for initialization
    void Start () 
	{
		
	}
    #endregion	
	// Update is called once per frame
	void Update () 
	{
		
	}
	public bool AddItem(LootItem loot)
    {
        //inventorySlots[0].itemConfig = loot.itemConfig;
        //inventorySlots[0].image.sprite = loot.itemConfig.itemIcon;
        //inventorySlots[0].ItemAmount = inventorySlots[0].ItemAmount + loot.ItemAmount;
        //inventorySlots[0].UpdateUI();
        return true;
    }

    //Editor Code Here
#if UNITY_EDITOR

#endif
}
