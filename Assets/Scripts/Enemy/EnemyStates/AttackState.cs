using UnityEngine;

[CreateAssetMenu (menuName = "States/Enemy/Attack State")]
public class AttackState : State<Enemy>
{
    [SerializeField] private string attackAnim;
    public override void Init(Enemy parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        Runner.animationHandler.PlayTargetAnim(attackAnim,0.25f);
    }

    public override void Update()
    {
        base.Update ();
        Runner.transform.LookAt(Runner.model.suspiciousLocation);
        if(Vector3.Distance(Runner.transform.position,Runner.model.suspiciousLocation.position) > Runner.model.attackRange) Runner.enemyStateMachine.SetState(typeof(ChaseState));
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        Runner.animationHandler.SetActionOff();
        Runner.StopAllCoroutines();
    }
}