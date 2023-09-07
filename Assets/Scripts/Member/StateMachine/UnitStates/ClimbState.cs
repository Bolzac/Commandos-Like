using System;
using UnityEngine;

[CreateAssetMenu (menuName = "States/Unit/Climb State")]
public class ClimbState : State<Member>
{
    [SerializeField] private string climbAnimation;
    public override void Init(Member parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        Runner.agentLinkMover.method = OffMeshLinkMoveMethod.Climb;
        Runner.animationHandler.PlayTargetAnim(climbAnimation,0.25f);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ChangeState()
    {
        
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