using System.Collections.Generic;
using UnityEngine;
public class UnitManager : MonoBehaviour
{
    public Camera cam;
    public TeamController teamController;
    public TeamModel teamModel;

    public Unit[] units;
    public List<Unit> selectedUnits;
    public Unit mainUnit;

    private void Start()
    {
        units = transform.GetComponentsInChildren<Unit>();
        for (var i = 0; i < units.Length; i++)
        {
            units[i].index = i;
        }
        SelectOneUnit(0);
        foreach (var unit in units)
        {
            unit.Init(this);
        }
        
        UIManager.instance.characterPanelHandler.InitPanel(units);
        UIManager.instance.skillPanelHandler.ShowPanel();
    }

    public void SelectMultipleUnit(int[] indexes)
    {
        foreach (var index in indexes)
        {
            if(selectedUnits.Contains(units[index])) continue;
            
            selectedUnits.Add(units[index]);
            units[index].controller.VisualizeSelected(true);
        }

        if (selectedUnits.Count > 0 && mainUnit != selectedUnits[0])
        {
            mainUnit = selectedUnits[0];
            OnMainUnitChanged();
        }
    }

    public void SelectOneUnit(int index)
    {
        if (mainUnit)
        {
            if (mainUnit.model.isSpeaking) return;
        }
        if(selectedUnits.Contains(units[index]) && selectedUnits.Count == 1) return;
        
        ClearAllSelected();
        selectedUnits.Add(units[index]);
        selectedUnits[0].controller.VisualizeSelected(true);
        mainUnit = selectedUnits[0];
        
        OnMainUnitChanged();
    }

    public void ClearAllSelected()
    {
        foreach (var selectedUnit in selectedUnits)
        {
            selectedUnit.controller.VisualizeSelected(false);
        }
        selectedUnits.Clear();
    }

    private void OnMainUnitChanged()
    {
        UIManager.instance.skillPanelHandler.SetSkills(mainUnit);
    }
}