using System;
using UnityEngine;

[CreateAssetMenu (menuName = "States/Unit/Jump State")]
public class JumpState : State<Member>
{
    [SerializeField] private string jumpAnimation;
    public override void Init(Member parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        Runner.agentLinkMover.method = OffMeshLinkMoveMethod.Parabola;
        Runner.animationHandler.PlayTargetAnim(jumpAnimation,0.25f);
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