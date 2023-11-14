using System;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public void Attack()
    {

    }

    public void ReactSound(Transform tr)
    {
        if(enemy.model.didSeeEnemy || enemy.model.canSeeEnemy) return;
        
        enemy.model.didHearAnything = true;
        enemy.model.suspiciousLocation = tr;
        enemy.enemyStateMachine.SetState(typeof(SuspiciousState));
    }

    public void Die(Member member)
    {
        enemy.agent.ResetPath();
        member.agent.ResetPath();
        enemy.transform.forward = member.transform.forward;
        member.transform.position = transform.GetChild(3).position;
        member.animationHandler.PlayTargetAnim(member.model.info.animationModel.stealthKill,0.25f);
        enemy.animationHandler.PlayTargetAnim(member.model.info.animationModel.stealthDie,0.25f);
        enemy.enemyStateMachine.SetState(typeof(DeadState));
    }
}