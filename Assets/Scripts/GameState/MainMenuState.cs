using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

[Serializable]
public class MainMenuState : GameState
{
    [SerializeField] private string sceneName;
    public override void Enter()
    {
        if(SceneManager.GetActiveScene().name != sceneName) runner.levelManager.LoadScene(sceneName);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}