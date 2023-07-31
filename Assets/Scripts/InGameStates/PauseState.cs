using System;
using UnityEngine.Events;

[Serializable]
public class PauseState : InGameBaseState
{
    public UnityEvent onEnterPauseState;
    public UnityEvent onExitPauseState;
    public override void Enter()
    {
        onEnterPauseState?.Invoke();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        onExitPauseState?.Invoke();
    }
}