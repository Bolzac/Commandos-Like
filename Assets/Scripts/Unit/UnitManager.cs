using System.Collections.Generic;
using UnityEngine;

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
        AddUnit(false,units[0]);
        foreach (var unit in units)
        {
            unit.Init(this);
        }
    }

    public void AddUnit(bool clear,Unit selected)
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

    public void SelectOneUnit(int index)
    {
        ClearAllSelected();
        selectedUnits.Clear();
        selectedUnits.Add(units[index]);
        selectedUnits[0].controller.VisualizeSelected(true);
    }

    public void ClearAllSelected()
    {
        foreach (var selectedUnit in selectedUnits)
        {
            selectedUnit.controller.VisualizeSelected(false);
        }
    }
}