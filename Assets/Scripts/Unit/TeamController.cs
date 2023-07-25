using System;
using System.Collections.Generic;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    private UnitManager _unitManager;
    private int _size;
    private int _angle;
    [HideInInspector] public List<Vector3> points;
    
    public float destinationRadius;

    private void Awake()
    {
        _unitManager = GetComponent<UnitManager>();
    }

    public void AssignDestinations(Vector3 destination)
    {
        _size = _unitManager.selectedUnits.Count;
        if (_size == 1)
        {
            _unitManager.selectedUnits[0].controller.Move(destination);
        }
        else
        {
            _angle = 360 / _size;
            points.Clear();
            for (int i = 0; i < _size; i++)
            {
                var dest = new Vector3(destination.x + destinationRadius * Mathf.Cos(_angle * i * Mathf.PI/180), 0, destination.z + destinationRadius * Mathf.Sin(_angle * i * Mathf.PI/180));
                points.Add(dest);
                _unitManager.selectedUnits[i].controller.Move(dest);
            }
            //Visualize destination points
        }
    }

    public void ToggleCrouchUnits()
    {
        bool isAllCrouching = true;
        foreach (var selectedUnit in _unitManager.selectedUnits)
        {
            if (!selectedUnit.model.isCrouching)
            {
                selectedUnit.controller.Crouch();
                isAllCrouching = false;
            }
        }

        if (isAllCrouching)
        {
            foreach (var selectedUnit in _unitManager.selectedUnits)
            {
                selectedUnit.controller.StandUp();
            }
        }
    }

    public void DisableRunning()
    {
        foreach (var selectedUnit in _unitManager.selectedUnits)
        {
            selectedUnit.model.isRunning = false;
        }
    }

    public void HandleRunning()
    {
        foreach (var selectedUnit in _unitManager.selectedUnits)
        {
            if(selectedUnit.model.isCrouching) selectedUnit.controller.StandUp();
            selectedUnit.model.isRunning = true;
        }
    }

    public void EnableSkill(int index)
    {
        _unitManager.selectedUnits[0].controller.SetReadySkill(index);
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
