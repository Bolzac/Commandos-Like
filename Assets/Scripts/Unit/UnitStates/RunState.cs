using UnityEngine;

[CreateAssetMenu (menuName = "States/Unit/Run State")]
public class RunState : State<Unit>
{
    [SerializeField] private float runningSpeed;
    public override void Init(Unit parent)
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