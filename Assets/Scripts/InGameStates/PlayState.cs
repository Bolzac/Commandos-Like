using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayState : InGameBaseState
{
    public UnityEvent onEnterPlayState;
    public UnityEvent onExitPlayState;
    public UnityEvent onUpdatePlayState;

    public override void Enter()
    {
        Time.timeScale = 1;
        onEnterPlayState?.Invoke(); // Enable UI and Inputs
    }

    public override void Update()
    {
        onUpdatePlayState.Invoke();
    }

    public override void Exit()
    {
        onExitPlayState?.Invoke(); //Disable UI and Inputs
    }
}