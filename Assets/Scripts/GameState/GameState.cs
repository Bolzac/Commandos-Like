public abstract class GameState
{
    protected GameManager runner;
    
    public void Init(GameManager gameManager)
    {
        runner = gameManager;
    }
    public abstract void Enter();

    public abstract void Update();

    public abstract void Exit();
}