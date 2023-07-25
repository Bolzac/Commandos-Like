using System.Collections;
using UnityEngine;

[CreateAssetMenu (menuName = "States/Enemy/Dead State")]
public class DeadState : State<Enemy>
{
    public string animName;
    public override void Init(Enemy parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        Runner.agent.ResetPath();
        Runner.model.capsuleCollider.enabled = false;
        Runner.fov.gameObject.SetActive(false);
        Runner.agent.enabled = false;
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