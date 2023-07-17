using System.Collections;
using UnityEngine;

[CreateAssetMenu (menuName = "States/Enemy/Suspicious State")]
public class SuspiciousState : State<Enemy>
{
    public string animName;
    public float lookDuration;
    public override void Init(Enemy parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        Runner.agent.ResetPath();
        Runner.transform.LookAt(Runner.model.suspiciousLocation);
        if (!Runner.model.canSeeEnemy)
        {
            Runner.model.canSeeSoundSource = Runner.fov.CanSeeSound();
            Runner.StartCoroutine(Wait());
        }
        else Runner.StartCoroutine(Runner.fov.IncreaseSuspiciousView());
    }

    public override void Update()
    {
        base.Update();
        Runner.transform.LookAt(Runner.model.suspiciousLocation);
        if(Runner.model.didSeeEnemy) Runner.enemyStateMachine.SetState(typeof(ChaseState));
        if(!Runner.model.canSeeEnemy && !Runner.model.didHearAnything) Runner.enemyStateMachine.SetState(typeof(PatrolState));
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        if(Runner.model.didSeeEnemy) Runner.animationHandler.PlayUpperBodyAnim(animName,0.25f);
        Runner.StopAllCoroutines();
    }
    
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(lookDuration);
        if (Runner.model.canSeeSoundSource)
        {
            Runner.model.didHearAnything = false;
            Runner.enemyStateMachine.SetState(typeof(PatrolState));
        }else Runner.enemyStateMachine.SetState(typeof(CheckState));
    }
}