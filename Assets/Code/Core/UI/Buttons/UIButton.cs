using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button),typeof(Image))]
public class UIButton : UIElement
{
    public enum ButtonType
    {
        DEFAULT,
        CONFIRM,
        DECLINE,
        WARNING
    }

    public ButtonType buttonType;
    
    private Image image;
    private Image icon;
    private Button button;

    public override void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        base.Awake();
    }

    protected override void OnStyleUI()
    {
        image = GetComponent<Image>();
        icon = transform.Find("Icon").GetComponent<Image>();
        button = GetComponent<Button>();

        button.transition = Selectable.Transition.SpriteSwap;
        button.targetGraphic = image;

        image.sprite = uiStyle.buttonSprite;
        image.type = Image.Type.Sliced;
        button.spriteState = uiStyle.buttonSpriteState;

        switch (buttonType)
        {
            case ButtonType.DEFAULT:
                image.color = uiStyle.defaultColor;
                icon.sprite = uiStyle.defaultIcon;
                break;
            case ButtonType.CONFIRM:
                image.color = uiStyle.confirmColor;
                icon.sprite = uiStyle.confirmIcon;
                break;
            case ButtonType.DECLINE:
                image.color = uiStyle.declineColor;
                icon.sprite = uiStyle.declineIcon;
                break;
            case ButtonType.WARNING:
                image.color = uiStyle.warningColor;
                icon.sprite = uiStyle.warningIcon;
                break;
            default:
                image.color = uiStyle.defaultColor;
                icon.sprite = uiStyle.defaultIcon;
                break;
        }
        base.OnStyleUI();
    }
}
