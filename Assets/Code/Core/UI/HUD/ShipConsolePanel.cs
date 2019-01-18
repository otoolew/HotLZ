using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipConsolePanel : MonoBehaviour 
{
    #region Variable Declarations
    public Button cargoButton;
    public Button equipmentButton;
    #endregion
    #region Initializations
    // Use this for initialization
    void Start () 
	{
        cargoButton.onClick.AddListener(HandleCargoClick);
        equipmentButton.onClick.AddListener(HandleEquipmentClick);
    }
    #endregion
    // Update is called once per frame
    void HandleCargoClick()
    {
        GameManager.Instance.TogglePause();
    }

    void HandleEquipmentClick()
    {
        GameManager.Instance.QuitToTitle();
    }

    //Editor Code Here
#if UNITY_EDITOR

#endif
}
