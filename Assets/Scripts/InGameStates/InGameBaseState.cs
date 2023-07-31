public abstract class InGameBaseState
{
    protected InGameManager runner;
    
    public virtual void Init(InGameManager inGameManager)
    {
        runner = inGameManager;
    }
    public abstract void Enter();

    public abstract void Update();

    public abstract void Exit();
}