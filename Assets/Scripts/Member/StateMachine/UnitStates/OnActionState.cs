using UnityEngine;

[CreateAssetMenu (menuName = "States/Unit/On Action")]
public class OnAction : State<Member>
{
    public override void Init(Member parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
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