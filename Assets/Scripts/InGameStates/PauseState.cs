using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PauseState : InGameBaseState
{
    public override void Enter()
    {
        runner.uiManager.StartPauseUI();

        Time.timeScale = 0;
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Time.timeScale = 1;
        runner.uiManager.ExitPauseUI();
    }
}