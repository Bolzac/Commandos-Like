using System.Collections;
using UnityEngine;

[CreateAssetMenu (menuName = "States/Enemy/Search State")]
public class SearchState : State<Enemy>
{
    [SerializeField] private LayerMask hidePlaceLayer;

    [SerializeField] private float rayDistance;

    [Header("Wall Statements")]
    [SerializeField] private bool currentLeftWall;
    [SerializeField] private bool previousLeftWall;
    
    [SerializeField] private bool currentRightWall;
    [SerializeField] private bool previousRightWall;

    [SerializeField] private float duration;
    [SerializeField] private int rotateTime;
    [SerializeField] private int maxRotateTime;
    private int _random;
    private bool _search;
    private float _counter;

    public override void Init(Enemy parent)
    {
        base.Init(parent);
    }

    public override void Enter()
    {
        base.Enter();
        SetDefaultAll();
        Runner.StartCoroutine(ReachDestination());
    }

    public override void Update()
    {
        base.Update();
        
        if(_search && maxRotateTime > rotateTime) Search();

        if(Runner.model.canSeeEnemy) Runner.enemyStateMachine.SetState(typeof(ChaseState));
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        Runner.agent.speed = Runner.model.walkingSpeed;
        Runner.StopAllCoroutines();
    }

    private IEnumerator ReachDestination()
    {
        Runner.agent.SetDestination(Runner.model.lastSeenLocation);
        yield return new WaitForSeconds(0.01f);
        yield return new WaitUntil(() => Runner.agent.remainingDistance < 0.1f);
        Runner.agent.ResetPath();
        Runner.agent.speed = Runner.model.walkingSpeed;
        _search = true;
    }

    private void SetDestinationToForward()
    {
        Runner.agent.ResetPath();
        Runner.agent.SetDestination(Runner.transform.position + Runner.transform.forward * 2);
    }

    private void Search()
    {
        if(Runner.agent.remainingDistance < 0.1f) SetDestinationToForward();
        _counter += Time.deltaTime;
        
        if(_counter < duration) return;
        _counter = 0;
        _random = Random.Range(0, 2);
        if (_random == 0)
        {
            RightRaycast();
            DetectCorridors();
        }
        else if(_random == 1)
        {
            LeftRaycast();
            DetectCorridors();
        }
    }


    //Raycastleri globalden locale çevir. +
    // Karakteri duvara paralel döndür
    private void LeftRaycast()
    {
        previousLeftWall = currentLeftWall;
        currentLeftWall = Physics.Raycast(Runner.model.eye.position, -Vector3.right,out RaycastHit hit, rayDistance, hidePlaceLayer);
    }

    private void RightRaycast()
    {
        previousRightWall = currentRightWall;
        currentRightWall = Physics.Raycast(Runner.model.eye.position, Runner.transform.right, rayDistance, hidePlaceLayer);
    }

    private void ForwardRaycast()
    {
        
    }

    private void Rotate(Vector3 direction)
    {
        rotateTime++;
        Runner.transform.LookAt(direction,Vector3.up);
        SetDestinationToForward();
    }

    private void DetectCorridors()
    {
        _random = Random.Range(0, 2);
        if (_random != 1)
        {
            if (!currentLeftWall && previousLeftWall)
            {
                Rotate(Runner.transform.position -Runner.transform.right);
                SetWallDefault();
            }
            else if(!currentRightWall && previousRightWall)
            {
                Rotate(Runner.transform.position + Runner.transform.right);
                SetWallDefault();
            }   
        }
    }

    private void SetWallDefault()
    {
        currentLeftWall = false;
        previousLeftWall = false;
        currentRightWall = false;
        previousRightWall = false;
    }

    private void SetDefaultAll()
    {
        SetWallDefault();
        rotateTime = 0;
        _search = false;
        _counter = 0;
    }
}