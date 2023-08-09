using UnityEngine;

[CreateAssetMenu (menuName = "States/Unit/Walk State")]
public class WalkState : State<Member>
{
    [SerializeField] private float walkingSpeed;
    public override void Init(Member parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        Runner.agent.speed = walkingSpeed;
        Runner.animationHandler.SetPatrolBlend(walkingSpeed);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ChangeState()
    {
        if(!Runner.agent.hasPath) Runner.stateManager.SetState(typeof(IdleState));
        else if(Runner.model.isCrouching) Runner.stateManager.SetState(typeof(CrouchState));
        else if(Runner.model.isRunning) Runner.stateManager.SetState(typeof(RunState));
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