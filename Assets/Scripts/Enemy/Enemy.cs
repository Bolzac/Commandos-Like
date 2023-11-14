using UnityEngine;
using UnityEngine.AI;

public class Enemy : Interactable
{
    public Camera cam;
    public AnimationHandler animationHandler;
    public NavMeshAgent agent;
    public EnemyStateManager enemyStateMachine;
    public FieldOfView fov;
    public EnemyModel model;
    public EnemyController controller;
    public EnemyView view;

    protected override void Awake()
    {
        base.Awake();
        hoverState = HoverState.Enemy;
        animationHandler = transform.GetChild(0).GetComponent<AnimationHandler>();
        agent = GetComponent<NavMeshAgent>();
        model = GetComponent<EnemyModel>();
        controller = GetComponent<EnemyController>();
        view = GetComponent<EnemyView>();
    }

    private void Start()
    {
        enemyStateMachine = GetComponent<EnemyStateManager>();
    }

    public override void Interaction(Member member)
    {
        controller.Die(member);
    }
}