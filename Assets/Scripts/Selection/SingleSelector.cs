using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SingleSelector
{
    public Camera cam;

    public UnityEvent<int> onSelectedOneUnit;
    public UnityEvent<Vector3> onSelectedDestination;
    public UnityEvent<IInteraction> onSelectedInteraction;

    public void SingleSelection()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity))
        {
            if (hit.transform.TryGetComponent(out Member unit))
            {
                onSelectedOneUnit?.Invoke(unit.index); //TeamManagement.SelectOneUnit
            }
            else
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    onSelectedDestination?.Invoke(hit.point); //TeamController.StopInteraction - TeamController.AssignDestinations
                }
                else if(hit.transform.TryGetComponent(out IInteraction interact))
                {
                    onSelectedInteraction?.Invoke(interact); //TeamController.StartInteraction
                }
            }
        }
    }
}