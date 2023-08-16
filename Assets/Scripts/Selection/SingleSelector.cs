using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[Serializable]
public class SingleSelector
{
    public Camera cam;

    public UnityEvent<int> onSelectedOneUnit;
    public UnityEvent<Vector3> onSelectedDestination;
    public UnityEvent<IInteraction> onSelectedInteraction;

    [SerializeField] private LayerMask layers;

    [Obsolete("Obsolete")]
    public void SingleSelection()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity,layers))
        {
            if (hit.transform.TryGetComponent(out Member unit))
            {
                onSelectedOneUnit?.Invoke(unit.index); //TeamManagement.SelectOneUnit
            }
            else if(hit.transform.TryGetComponent(out IInteraction interact))
            {
                onSelectedInteraction?.Invoke(interact); //TeamController.StartInteraction
            }else if (NavMesh.SamplePosition(hit.point,out NavMeshHit navMeshHit,0.2f,NavMesh.GetNavMeshLayerFromName("Ground")))
            {
                onSelectedDestination?.Invoke(navMeshHit.position); //TeamController.StopInteraction - TeamController.AssignDestinations
            }
        }
    }
}