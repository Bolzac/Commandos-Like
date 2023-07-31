using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class LevelSelectionState : GameState
{
    public GameObject levelSelectionCanvas;
    public override void Enter()
    {
        Object.Instantiate(levelSelectionCanvas);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
    }
}