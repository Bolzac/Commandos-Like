using System.Collections;
using UnityEngine;

[CreateAssetMenu (menuName = "States/Enemy/Check State")]
public class CheckState : State<Enemy>
{
    public float waitDuration;
    public override void Init(Enemy parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        Runner.agent.SetDestination(Runner.model.suspiciousLocation.position);
        Runner.StartCoroutine(Wait());
    }

    public override void Update()
    {
        base.Update();
        Runner.model.canSeeSoundSource = Runner.fov.CanSeeSound();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        Runner.StopAllCoroutines();
    }

    private IEnumerator Wait()
    {
        while (!Runner.model.canSeeSoundSource)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Runner.agent.ResetPath();
        Runner.model.didHearAnything = false;
        yield return new WaitForSeconds(waitDuration);
        Runner.enemyStateMachine.SetState(typeof(PatrolState));
    }
}