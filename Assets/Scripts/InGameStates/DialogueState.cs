using System;
using UnityEngine.Events;

[Serializable]
public class DialogueState : InGameBaseState
{
    public override void Enter()
    {
        runner.uiManager.StartDialogueUI();
        runner.animator.Play("DialogueCamera");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        runner.uiManager.ExitDialogueUI();
    }
}