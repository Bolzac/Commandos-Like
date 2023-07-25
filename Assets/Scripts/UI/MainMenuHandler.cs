using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionPanel;

    public void SelectPlay()
    {
        GameManager.Instance.sceneHandler.OpenDemoScene();
    }

    public void SelectOptions()
    {
        mainMenuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        optionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}