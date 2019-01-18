using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newKeyMap", menuName = "KeyCode")]
public class KeyCodeVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public KeyCode KeyAssignment;

    public bool KeyDownValue()
    {
        if (Input.GetKeyDown(KeyAssignment))
            return true;
        else
            return false;
    }
    public bool KeyUpValue()
    {
        if (Input.GetKeyUp(KeyAssignment))
            return true;
        else
            return false;
    }
    public bool KeyPressValue()
    {
        if (Input.GetKey(KeyAssignment))
            return true;
        else
            return false;
    }
}
