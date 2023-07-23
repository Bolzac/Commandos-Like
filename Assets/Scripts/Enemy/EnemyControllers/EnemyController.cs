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

    public void Die()
    {
        enemy.enemyStateMachine.SetState(typeof(DeadState));
    }
}