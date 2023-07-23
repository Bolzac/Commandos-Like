using UnityEngine;

[CreateAssetMenu (menuName = "States/Unit/Walk State")]
public class WalkState : State<Unit>
{
    [SerializeField] private float walkingSpeed;
    public override void Init(Unit parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        Runner.agent.speed = walkingSpeed;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ChangeState()
    {
        if(!Runner.agent.hasPath) Runner.stateManager.SetState(typeof(IdleState));
        else if(Runner.model.isCrouching) Runner.stateManager.SetState(typeof(WalkState));
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