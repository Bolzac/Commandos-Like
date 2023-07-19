using UnityEngine;

[CreateAssetMenu (menuName = "States/Unit/Idle State")]
public class IdleState : State<Unit>
{
    public override void Init(Unit parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        //if(Runner.agent) Runner.agent.ResetPath();
    }

    public override void Update()
    {
        base.Update();
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

