using System;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
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
        enemy.transform.forward = member.transform.forward;
        enemy.transform.position = enemy.transform.forward * member.model.info.animationModel.stealthAnimDistance + member.transform.position;
        member.animationHandler.PlayTargetAnim(member.model.info.animationModel.stealthKill,0.25f);
        enemy.animationHandler.PlayTargetAnim(member.model.info.animationModel.stealthDie,0.25f);
        enemy.enemyStateMachine.SetState(typeof(DeadState));
    }
}