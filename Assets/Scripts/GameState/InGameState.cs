using System;

[Serializable]
public class InGameState : GameState
{
    public override void Enter()
    {
        runner.inGameManager.gameObject.SetActive(true); //Oyun içine girdiğimde oyun içindeki stateleri yönetecek child objesini aktifleştirdim.
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}