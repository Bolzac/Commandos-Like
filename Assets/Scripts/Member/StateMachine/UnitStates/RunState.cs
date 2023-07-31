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
    }

    public override void Update()
    {
        base.Update();
        //Runner.controller.CreateNoise();
        Runner.animationHandler.SetPatrolBlend(Runner.agent.velocity.magnitude);
    }
    
    public override void ChangeState()
    {
        if(Runner.model.isCrouching) Runner.stateManager.SetState(typeof(CrouchState));
        else if(!Runner.agent.hasPath) Runner.stateManager.SetState(typeof(IdleState));
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