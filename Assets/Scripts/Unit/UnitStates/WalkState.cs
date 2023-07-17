using UnityEngine;

[CreateAssetMenu (menuName = "States/Unit/Walk State")]
public class WalkState : State<Unit>
{
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float crouchingSpeed;
    [SerializeField] private float distThreshold;
    public override void Init(Unit parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        Runner.agent.speed = Runner.model.isCrouching ? crouchingSpeed : walkingSpeed;
    }

    public override void Update()
    {
        base.Update();
        //if(Runner.agent.remainingDistance < distThreshold) Runner.stateMachine.SetState(typeof(IdleState));
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