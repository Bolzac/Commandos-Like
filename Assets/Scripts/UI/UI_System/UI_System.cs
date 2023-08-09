using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_System : MonoBehaviour
{
    #region Variables

    [Header("System Events")] 
    public UnityEvent onSwitchedScreen;

    [Header("Fader Properties")] 
    public Image fader;

    public float fadeInDuration;
    public float fadeOutDuration;
    
    private UI_Screen[] _screens = Array.Empty<UI_Screen>();

    public UI_Screen previousScreen;
    public UI_Screen currentScreen;

    #endregion

    #region Main Methods

    private void Start()
    {
        _screens = GetComponentsInChildren<UI_Screen>(true);

        if (!fader) return;
        fader.gameObject.SetActive(true);
        FadeIn();
        SwitchScreens(_screens[0]);
    }

    #endregion

    #region Helper Methods

    public void FadeIn()
    {
        if (fader) fader.CrossFadeAlpha(0f,fadeInDuration,false);
    }
    
    public void FadeOut()
    {
        if (fader) fader.CrossFadeAlpha(1,fadeOutDuration,false);
    }

    public void SwitchScreens(UI_Screen aScreen)
    {
        if (aScreen)
        {
            if (currentScreen)
            {
                currentScreen.CloseScreen();
                previousScreen = currentScreen;
            }

            currentScreen = aScreen;
            currentScreen.StartScreen();
            
            onSwitchedScreen?.Invoke();
        }
    }

    public void GoToPreviousScreen()
    {
        if (previousScreen) SwitchScreens(previousScreen);
    }

    public void LoadScene(string sceneName)
    {
        GameManager.Instance.levelManager.LoadScene(sceneName);
    }

    #endregion
}
