using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode()]
public class UIElement : MonoBehaviour
{
    public UIStyle uiStyle;

    protected virtual void OnStyleUI()
    {

    }
    public virtual void Awake()
    {
        OnStyleUI();
    }
    public virtual void Update()
    {
        if (Application.isEditor)
        {
            OnStyleUI();
        }
    }

}
