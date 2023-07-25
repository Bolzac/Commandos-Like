using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler
{
    public void OpenDemoScene()
    {
        
    }
    public void ChangeScene(int index)
    {
        Debug.Log("Changing the scene");
        SceneManager.LoadScene(index);
    }

    public void BackToMainMenu()
    {
        Debug.Log("Back to main menu");
        ChangeScene(0);
    }

    public void OpenLevelSelection()
    {
        Debug.Log("Opening the level selection");
    }
}