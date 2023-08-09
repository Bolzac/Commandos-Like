using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PauseState : InGameBaseState
{
    public UnityEvent onEnterPauseState;
    public UnityEvent onExitPauseState;
    public override void Enter()
    {
        onEnterPauseState?.Invoke();

        Time.timeScale = 0;
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Time.timeScale = 1;
        onExitPauseState?.Invoke();
    }
}