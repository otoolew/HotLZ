using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Inventory Item")]
public class ItemConfig : ScriptableObject 
{
    #region Variable Declarations
    public string itemName = "New Item";
    public Enums.ItemType itemType;
    public Sprite itemIcon = null;
    #endregion

}
