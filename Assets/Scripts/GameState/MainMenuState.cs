using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class MainMenuState : GameState
{
    public GameObject mainMenuCanvas;
    public override void Enter()
    {
        Object.Instantiate(mainMenuCanvas);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
    }
}