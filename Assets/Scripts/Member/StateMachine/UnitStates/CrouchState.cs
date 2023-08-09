using UnityEngine;

[CreateAssetMenu (menuName = "States/Unit/Crouch State")]
public class CrouchState : State<Member>
{
    [SerializeField] private float crouchingSpeed;
    public override void Init(Member parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        Runner.agent.speed = crouchingSpeed;
        Runner.animationHandler.SetPatrolBlend(crouchingSpeed);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ChangeState()
    {
        if(!Runner.agent.hasPath) Runner.stateManager.SetState(typeof(IdleState));
        else if (!Runner.model.isCrouching) Runner.stateManager.SetState(typeof(WalkState));
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