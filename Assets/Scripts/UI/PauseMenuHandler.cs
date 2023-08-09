using System;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenuHandler : MonoBehaviour
{
    public UnityEvent<Type> onResume;
    public void Resume()
    {
        onResume.Invoke(typeof(PlayState));
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.SetState(typeof(MainMenuState));
    }
}