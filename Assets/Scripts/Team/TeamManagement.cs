using System;
using System.Collections.Generic;
using UnityEngine;

public class TeamManagement : MonoBehaviour
{
    public static TeamManagement Instance;

    public Member[] members;
    public List<Member> selectedUnits;
    public event Action<Member> OnMemberChanged;

    private void Awake()
    {
        Instance = this;
        members = transform.GetComponentsInChildren<Member>();

        foreach (Member mem in members)
        {
            mem.OnSelect += selectable => selectable.Select();
        }
        
        SelectOneUnit(members[0].index);
        
        for (var i = 0; i < members.Length; i++)
        {
            members[i].index = i;
        }
    }

    public void SelectMultipleUnit(int[] indexes)
    {
        ClearAllSelected();
        foreach (var index in indexes)
        {
            selectedUnits.Add(members[index]);
            members[index].controller.VisualizeSelected(true);
        }
    }

    public void SelectOneUnit(int index)
    {
        if(selectedUnits.Contains(members[index]) && selectedUnits.Count == 1) return;
        
        ClearAllSelected();
        
        selectedUnits.Add(members[index]);
        
        selectedUnits[0].controller.VisualizeSelected(true);
        OnMemberChanged?.Invoke(selectedUnits[0]);
    }

    private void ClearAllSelected()
    {
        foreach (var selectedUnit in selectedUnits)
        {
            selectedUnit.controller.VisualizeSelected(false);
        }
        selectedUnits.Clear();
    }

}