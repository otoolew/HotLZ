// ----------------------------------------------------------------------------
//  William O'Toole 
//  Project: Starship
//  SEPT 2018
// ----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour 
{
    public Button ResumeButton;
    public Button QuitButton;

    private void Awake()
    {
        ResumeButton.onClick.AddListener(HandleButtonClick);
        QuitButton.onClick.AddListener(HandleQuitClick);
    }

    void HandleButtonClick()
    {
        GameManager.Instance.TogglePause();
    }


    void HandleQuitClick()
    {
        GameManager.Instance.QuitToTitle();
    }
}
