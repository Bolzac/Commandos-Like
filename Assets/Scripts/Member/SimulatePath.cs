using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class SimulatePath : MonoBehaviour
{
    #region Main

    [SerializeField] private float drawUpdateSpeed;
    private Camera cam;
    private NavMeshPath _navMeshPath;
    private List<Vector3> corners;
    
    #endregion
    
    #region Parabolic Line

    [SerializeField] private int segments;
    [SerializeField] private float heightMultiplier;
    private LineRenderer linePath;
    private Vector3 midPoint; 
    private bool draw;

    #endregion

    #region QuadraticBezierPoints

    private float u;
    private float tt;
    private float uu;

    #endregion

    [Header("Design Test")] 
    [SerializeField] private bool drawParabolic;
    [SerializeField] private bool changeCursor;

    private void Awake()
    {
        cam = Camera.main;
        linePath = GetComponent<LineRenderer>();
        _navMeshPath = new NavMeshPath();
    }

    private void Start()
    {
        StartCoroutine(FindEndDestination());
    }

    private IEnumerator FindEndDestination()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit navMeshHit, 0.2f, NavMesh.AllAreas))
                {
                    CalculatePath(navMeshHit);
                }   
            }
            yield return new WaitForSeconds(drawUpdateSpeed);
        }
    }

    private void CalculatePath(NavMeshHit navMeshHit)
    {
        if (NavMesh.CalculatePath(TeamManagement.Instance.selectedUnits[0].transform.position, navMeshHit.position, NavMesh.AllAreas, _navMeshPath))
        {
            draw = false;
            corners = new List<Vector3>(_navMeshPath.corners);
            for (int i = 0; i < corners.Count-1; i++)
            {
                if (Physics.Raycast(corners[i], Vector3.up, 1))
                {
                    if (Physics.Raycast(corners[i + 1], Vector3.up, 1))
                    {
                        draw = true;
                        if(drawParabolic) DrawParabolicLine(corners[i], corners[i+1]);
                        if(changeCursor) ChangeCursor();
                    }
                }
            }
            linePath.enabled = draw;
        }
    }

    private void DrawParabolicLine(Vector3 startPoint, Vector3 endPoint)
    {
        midPoint = (startPoint + endPoint) / 2;
        midPoint.y += Vector3.Distance(startPoint, endPoint) * heightMultiplier;
        
        for (int i = 0; i <= segments; i++)
        {
            linePath.SetPosition(i,CalculateQuadraticBezierPoint(startPoint, midPoint, endPoint, i / (float)segments));
        }
        linePath.positionCount = segments + 1;
    }

    private void ChangeCursor()
    {
        
    }
    
    private Vector3 CalculateQuadraticBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        u = 1 - t;
        tt = t * t;
        uu = u * u;
        
        return uu * p0 + 2 * u * t * p1 + tt * p2;
    }

    /*
     * Ya parabolik bir çizgi çizilecek zıplama yerlerine
     * Ya da mouse imleci gidilip gidilemeyeceğini gösterecek
     * Zıplama yerleri farklı bir şekilde gösterilebilir. Start position spesifik olur fakat end position değiştirilir
     */
}