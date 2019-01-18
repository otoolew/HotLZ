using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour 
{
    #region Variable Declarations

    public ItemConfig itemConfig;
    public Sprite sprite;
    public int ItemAmount;
    #endregion
    #region Initializations
    // Use this for initialization
    void Start () 
	{
        sprite = itemConfig.itemIcon;
        GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }
    #endregion	
	// Update is called once per frame
	void Update () 
	{
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (InventoryManager.Instance.AddItem(this))
            {

                Destroy(gameObject);
            }

        }
    }
    //Editor Code Here
#if UNITY_EDITOR

#endif
}
