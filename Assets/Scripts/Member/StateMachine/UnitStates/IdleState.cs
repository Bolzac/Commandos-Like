using UnityEngine;

[CreateAssetMenu (menuName = "States/Unit/Idle State")]
public class IdleState : State<Member>
{
    public override void Init(Member parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        if(Runner.animationHandler.animator) Runner.animationHandler.SetPatrolBlend(0);
    }

    public override void Update()
    {
        base.Update();
    }
    
    public override void ChangeState()
    {
        if (Runner.agent.hasPath)
        {
            if(Runner.model.isRunning) Runner.stateManager.SetState(typeof(RunState));
            else if(Runner.model.isCrouching) Runner.stateManager.SetState(typeof(CrouchState));
            else Runner.stateManager.SetState(typeof(WalkState));
        }
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

