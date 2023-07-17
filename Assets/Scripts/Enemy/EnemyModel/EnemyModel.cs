using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyModel : MonoBehaviour
{
    public EnemyType enemyType;

    public Transform eye;

    public List<Transform> waypoints;
    public float attackRange;

    [Header("Speed Variables")] 
    public float walkingSpeed;
    public float runningSpeed;
    public float searchSpeed;
    
    [Header("States")]
    public bool canSeeEnemy;
    public bool didSeeEnemy;

    public bool didHearAnything;
    public bool canSeeSoundSource;

    public Transform suspiciousLocation;
    public Vector3 lastSeenLocation;

    public Vector3 destination;

    [Header("Gizmos")]
    public bool patrolGizmos;
    public bool searchGizmos;
}

public enum EnemyType
{
    Robot,
    Creature,
    Normal
}