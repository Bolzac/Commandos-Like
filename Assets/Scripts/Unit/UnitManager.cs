using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitManager : MonoBehaviour
{
    public Camera cam;
    public InputHandler inputHandler;
    public TeamController teamController;
    public TeamModel teamModel;

    public Unit[] units;
    public List<Unit> selectedUnits;

    private void Awake()
    {
        teamModel = GetComponent<TeamModel>();
        teamController = GetComponent<TeamController>();
        units = transform.GetComponentsInChildren<Unit>();
        AddOneUnit(false,units[0]);
        foreach (var unit in units)
        {
            unit.Init(this);
        }
    }

    public void AddOneUnit(bool clear,Unit selected)
    {
        if(!selected) return;
        if (clear)
        {
            ClearAllSelected();
            selectedUnits.Clear();
        }
        if (!selectedUnits.Contains(selected))
        {
            selectedUnits.Add(selected);
            selectedUnits[^1].controller.VisualizeSelected(true);
        }
    }

    public void ClearAllSelected()
    {
        foreach (var selectedUnit in selectedUnits)
        {
            selectedUnit.controller.VisualizeSelected(false);
        }
    }

    public void SelectMultipleUnits()
    {
        
    }
}