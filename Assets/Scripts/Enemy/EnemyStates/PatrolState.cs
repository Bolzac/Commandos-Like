using System.Collections;
using UnityEngine;

[CreateAssetMenu (menuName = "States/Enemy/Patrol State")]
public class PatrolState : State<Enemy>
{
    [SerializeField] private bool isLooping;
    [SerializeField] private float waitDuration;
    [SerializeField] private float destinationThreshold;
    [SerializeField] private int waypointOrder;
    [SerializeField] private Material patrolMaterial;
    private bool _isReturning;
    public override void Init(Enemy parent)
    {
        base.Init(parent);
        waypointOrder = 0;
        _isReturning = false;
    }

    public override void Enter()
    {
        base.Enter();
        Runner.agent.speed = Runner.model.walkingSpeed;
        Runner.fov.viewMeshRenderer.material = patrolMaterial;
        Runner.StartCoroutine(Patrol());
    }

    public override void Update()
    {
        base.Update();
        if(Runner.model.canSeeEnemy) Runner.enemyStateMachine.SetState(typeof(SuspiciousState));
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        Runner.StopAllCoroutines();
        Runner.animationHandler.SetPatrolBlend(0);
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            Runner.animationHandler.SetPatrolBlend(0);
            yield return new WaitForSeconds(waitDuration);
            Runner.agent.SetDestination(Runner.model.waypoints[waypointOrder].position);
            yield return new WaitForSeconds(0.1f);
            Runner.animationHandler.SetPatrolBlend(Runner.model.walkingSpeed);
            yield return new WaitUntil(() => Runner.agent.pathEndPosition == Runner.transform.position);

            if (isLooping)
            {
                waypointOrder += 1;
                if (waypointOrder == Runner.model.waypoints.Count) waypointOrder = 0;
            }
            else
            {
                if (_isReturning)
                {
                    waypointOrder -= 1;
                    if (waypointOrder == 0) _isReturning = false;
                }
                else
                {
                    waypointOrder += 1;
                    if (waypointOrder == Runner.model.waypoints.Count - 1) _isReturning = true;
                }   
            }
        }
    }
}