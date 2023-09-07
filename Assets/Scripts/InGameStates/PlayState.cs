using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayState : InGameBaseState
{
    public UnityEvent onUpdatePlayState;

    public override void Enter()
    {
        Time.timeScale = 1;
        runner.inputManager.inputModel.ResetVariables();
        runner.uiManager.StartPlayUI();
        runner.inputManager.EnablePlay();
        runner.animator.Play("IsometricFreeCamera");
    }

    public override void Update()
    {
        if(GameManager.Instance.isOverUI) return; 
        if(!TeamManagement.Instance.selectedUnits[0].model.isSkillEnable) runner.selectionManager.ObserveMouseBehaviour();
        onUpdatePlayState.Invoke();
    }

    public override void Exit()
    {
        runner.uiManager.ExitPlayUI();
        runner.inputManager.DisablePlay();
    }
}