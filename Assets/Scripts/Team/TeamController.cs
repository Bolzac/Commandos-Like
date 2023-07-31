using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeamController : MonoBehaviour
{
    public TeamManagement teamManagement;
    
    private int _size;
    private int _angle;
    [HideInInspector] public List<Vector3> points;
    
    public float destinationRadius;
    private Coroutine _coroutine;

    private void Awake()
    {
        teamManagement = GetComponent<TeamManagement>();
    }

    public void StartInteraction(IInteraction interaction)
    {
        _coroutine = StartCoroutine(teamManagement.mainMember.controller.Interaction(interaction));
    }

    public void StopInteraction()
    {
        if(_coroutine != null) StopCoroutine(_coroutine);
    }

    public void AssignDestinations(Vector3 destination)
    {
        _size = teamManagement.selectedUnits.Count;
        if (_size == 1)
        {
            teamManagement.selectedUnits[0].controller.Move(destination);
        }
        else
        {
            _angle = 360 / _size;
            points.Clear();
            for (int i = 0; i < _size; i++)
            {
                var dest = new Vector3(destination.x + destinationRadius * Mathf.Cos(_angle * i * Mathf.PI/180), 0, destination.z + destinationRadius * Mathf.Sin(_angle * i * Mathf.PI/180));
                points.Add(dest);
                teamManagement.selectedUnits[i].controller.Move(dest);
            }
            //Visualize destination points
        }
    }

    public void ToggleCrouchUnits()
    {
        bool isAllCrouching = true;
        foreach (var selectedUnit in teamManagement.selectedUnits)
        {
            if (!selectedUnit.model.isCrouching)
            {
                selectedUnit.controller.Crouch();
                isAllCrouching = false;
            }
        }

        if (isAllCrouching)
        {
            foreach (var selectedUnit in teamManagement.selectedUnits)
            {
                selectedUnit.controller.StandUp();
            }
        }
    }

    public void DisableRunning()
    {
        foreach (var selectedUnit in teamManagement.selectedUnits)
        {
            selectedUnit.model.isRunning = false;
        }
    }

    public void HandleRunning()
    {
        foreach (var selectedUnit in teamManagement.selectedUnits)
        {
            if(selectedUnit.model.isCrouching) selectedUnit.controller.StandUp();
            selectedUnit.model.isRunning = true;
        }
    }

    public void EnableSkill(int index)
    {
        teamManagement.selectedUnits[0].controller.SetReadySkill(index);
    }

    private void OnDrawGizmos()
    {
        if(points.Count == 0) return;
        foreach (var point in points)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(point,0.3f);
        }
    }
}
