using UnityEngine;

[CreateAssetMenu (menuName = "States/Unit/Run State")]
public class RunState : State<Member>
{
    [SerializeField] private float runningSpeed;
    public override void Init(Member parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        if(Runner.model.isCrouching) Runner.controller.StandUp();
        Runner.agent.speed = runningSpeed;
        Runner.animationHandler.SetPatrolBlend(runningSpeed);
    }

    public override void Update()
    {
        base.Update();
        //Runner.controller.CreateNoise();
    }
    
    public override void ChangeState()
    {
        if(Runner.model.isCrouching) Runner.stateManager.SetState(typeof(CrouchState));
        else if(!Runner.agent.hasPath) Runner.stateManager.SetState(typeof(IdleState));
        else if(!Runner.model.isRunning) Runner.stateManager.SetState(typeof(WalkState));
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}