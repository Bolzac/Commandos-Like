using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class FieldOfView : MonoBehaviour
{
    [FormerlySerializedAs("enemy")] public Enemy unit;
    [Header("View Values")] 
    public float viewRadius;
    [Range(0, 360)] public float viewAngle;

    [Header("Layer Masks")] public LayerMask targetMask;
    public LayerMask obstacleMask;

    [Header("Visualisation of View")] 
    public float meshResolution;
    public MeshFilter viewMeshFilter;
    public Mesh viewMesh;
    public MeshRenderer viewMeshRenderer;

    public bool showViewCone;


    private Collider[] _visibleTargets;
    [HideInInspector] public List<Transform> visibleTargets;

    public float perTime;
    public float radiusChangeSpeed;
    public float currentRadius;
    public Collider[] seenEnemies;

    private void Awake()
    {
        unit = transform.parent.GetComponent<Enemy>();
    }

    void Start()
    {
        CreateMesh();
        _visibleTargets = new Collider[4];
        seenEnemies = new Collider[4];
        StartCoroutine(nameof(FindTargetsWithDelay), .2f);
    }

    private void Update()
    {
        ActivateDrawField();
    }

    private void LateUpdate()
    {
        if (showViewCone) DrawFieldOfView();
    }

    private IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    public IEnumerator IncreaseSuspiciousView()
    {
        int size = 0;
        while (size == 0)
        {
            yield return new WaitForSeconds(perTime);
            unit.model.didSeeEnemy = false;
            currentRadius = Mathf.Clamp(currentRadius + radiusChangeSpeed, 0, viewRadius);
            size = Physics.OverlapSphereNonAlloc(transform.position, currentRadius, seenEnemies, targetMask);
        }

        unit.model.didSeeEnemy = true;
    }

    private void FindVisibleTargets()
    {
        visibleTargets.Clear();
        unit.model.canSeeEnemy = false;
        var size = Physics.OverlapSphereNonAlloc(transform.position, viewRadius, _visibleTargets, targetMask);
        if (size > 0)
        {
            for (int i = 0; i < size; i++)
            {
                Transform target = _visibleTargets[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    if (!Physics.Raycast(transform.position, dirToTarget, viewRadius, obstacleMask))
                    {
                        visibleTargets.Add(target);
                        unit.model.canSeeEnemy = true;
                    }
                }
            }
        }

        if (visibleTargets.Count == 0) unit.model.canSeeEnemy = false;

        if (unit.model.canSeeEnemy) FindClosestTarget();
    }

    //If object hear something and can not see the source, we are going to send ray to that location. If raycast doesn't hit anything it means we can see source
    public bool CanSeeSound()
    {
        Vector3 dirToTarget = (unit.model.suspiciousLocation.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
        {
            return !Physics.Raycast(transform.position, dirToTarget, viewRadius, obstacleMask);   
        }

        return false;
    }

    private void FindClosestTarget()
    {
        if (visibleTargets.Count > 1)
        {
            Transform closestTarget = visibleTargets[0];
            for (var i = 1; i < visibleTargets.Count; i++)
            {
                if (Vector3.Distance(unit.transform.position, closestTarget.position) >
                    Vector3.Distance(unit.transform.position, visibleTargets[i].position))
                {
                    closestTarget = visibleTargets[i];
                }
            }

            unit.model.suspiciousLocation = closestTarget;
        }
        else unit.model.suspiciousLocation = visibleTargets[0];
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void DrawFieldOfView()
    {
        int rayCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float rayAngleSize = viewAngle / rayCount;
        ViewCastInfo newViewCast;
        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + rayAngleSize * i;
            newViewCast = ViewCast(angle,viewRadius);
            viewPoints.Add(newViewCast.Point);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (vertexCount - 2 > i)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
    }

    private ViewCastInfo ViewCast(float globalAngle,float radius)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position,dir,out hit,radius,obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }

        return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
    }

    private struct ViewCastInfo
    {
        public bool Hit;
        public Vector3 Point;
        public float Dst;
        public float Angle;

        public ViewCastInfo(bool hit, Vector3 point, float dst, float angle)
        {
            Hit = hit;
            Point = point;
            Dst = dst;
            Angle = angle;
        }
    }

    private void ActivateDrawField()
    {
        if (Input.GetMouseButtonUp(1))
        {
            var ray = unit.cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit hit) && unit.transform == hit.transform)
            {
                showViewCone = !showViewCone;
                viewMeshRenderer.enabled = showViewCone;
            }
        }

        if (visibleTargets.Count > 0)
        {
            showViewCone = true;
            viewMeshRenderer.enabled = true;
        }
    }

    private void CreateMesh()
    {
        viewMesh = new Mesh
        {
            name = "ViewMesh"
        };
        viewMeshFilter.mesh = viewMesh;
    }
}