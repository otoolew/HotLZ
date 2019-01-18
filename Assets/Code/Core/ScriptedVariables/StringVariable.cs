// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// Modified: William O'Toole
// ----------------------------------------------------------------------------
using UnityEngine;
[CreateAssetMenu(fileName = "newStringVar", menuName = "Variables/String")]
public class StringVariable : ScriptableObject
{
    [SerializeField]
    private string value = "";

    public string Value
    {
        get { return value; }
        set { this.value = value; }
    }  
}
