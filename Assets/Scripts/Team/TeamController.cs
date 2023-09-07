using System.Collections.Generic;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    public TeamManagement teamManagement;

    private int _size;
    private int _angle;
    [HideInInspector] public List<Vector3> points;
    
    public float destinationRadius;
    public GameObject destinationIndicator;
    private Vector3 destinationPoint;

    private void Start()
    {
        teamManagement = GetComponent<TeamManagement>();

        FindObjectOfType<InGameManager>().selectionManager.destinationSelection.OnSelect += AssignDestinations;
    }

    public void AssignDestinations(Vector3 destination)
    {
        _size = teamManagement.selectedUnits.Count;
        if (_size == 1)
        {
            destinationPoint = destination;
            teamManagement.selectedUnits[0].controller.Move(destinationPoint);
        }
        else
        {
            _angle = 360 / _size;
            points.Clear();
            for (int i = 0; i < _size; i++)
            {
                destinationPoint = new Vector3(destination.x + destinationRadius * Mathf.Cos(_angle * i * Mathf.PI/180), destination.y, destination.z + destinationRadius * Mathf.Sin(_angle * i * Mathf.PI/180));
                points.Add(destinationPoint);
                teamManagement.selectedUnits[i].controller.Move(destinationPoint);
            }
        }
    }

    public void ToggleCrouchUnits()
    {
        bool isAllCrouching = true;
        foreach (var selectedUnit in teamManagement.selectedUnits)
        {
            if (!selectedUnit.model.isCrouching)
            {
                selectedUnit.controller.toCrouch.Start();
                isAllCrouching = false;
            }
        }

        if (isAllCrouching)
        {
            foreach (var selectedUnit in teamManagement.selectedUnits)
            {
                selectedUnit.controller.toStandUp.Start();
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
            if(selectedUnit.model.isCrouching) selectedUnit.controller.toStandUp.Start();
            selectedUnit.model.isRunning = true;
        }
    }
}