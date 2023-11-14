using System;
using UnityEngine;
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
        runner.cameraController.StartFocusCoroutine(0);
        runner.uiManager.ExitDialogueUI();
    }
}