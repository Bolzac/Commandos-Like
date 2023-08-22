using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class DestinationSelection
{
    [SerializeField] private Camera cam;
    public event Action<Vector3> OnSelect;
    [SerializeField] private LayerMask layers;

    public void SelectDestination()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layers)) return;
        
        if (NavMesh.SamplePosition(hit.point,out NavMeshHit navMeshHit,0.2f,NavMesh.GetAreaFromName("Ground")))
        {
            OnSelect?.Invoke(navMeshHit.position);
        }
    }
}