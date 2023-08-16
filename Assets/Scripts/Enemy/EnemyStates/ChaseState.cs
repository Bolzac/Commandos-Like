using UnityEngine;

[CreateAssetMenu (menuName = "States/Enemy/Chase State")]
public class ChaseState : State<Enemy>
{
    [SerializeField] private float chaseSpeed;
    private Vector3 _temp;
    public override void Init(Enemy parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        Runner.agent.speed = Runner.model.runningSpeed;
        _temp = Runner.model.suspiciousLocation.position;
        Runner.agent.SetDestination(_temp);
        Runner.animationHandler.SetPatrolBlend(Runner.model.runningSpeed);
    }

    public override void Update()
    {
        base.Update();

        if (_temp != Runner.model.suspiciousLocation.position)
        {
            _temp = Runner.model.suspiciousLocation.position;
            Runner.agent.SetDestination(_temp);
        }
        
        Runner.transform.LookAt(Runner.model.suspiciousLocation);
        if(Runner.agent.remainingDistance < Runner.model.attackRange) Runner.enemyStateMachine.SetState(typeof(AttackState));
        if (!Runner.model.canSeeEnemy)
        {
            Runner.model.lastSeenLocation = Runner.model.suspiciousLocation.position;
            Runner.enemyStateMachine.SetState(typeof(SearchState));
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        Runner.agent.ResetPath();
        Runner.StopAllCoroutines();
    }
}