using System;
using UnityEngine.Events;

[Serializable]
public class DialogueState : InGameBaseState
{
    public UnityEvent onEnterDialogueState;
    public UnityEvent onExitDialogueState;
    public override void Enter()
    {
        onEnterDialogueState?.Invoke();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        onExitDialogueState?.Invoke();
    }
}