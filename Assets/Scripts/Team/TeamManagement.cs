using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeamManagement : Child
{
    [SerializeField] private UnityEvent<Member[]> onInit;
    [SerializeField] private UnityEvent<Member> onMainMemberChanges;

    public Member[] members;
    public List<Member> selectedUnits;
    public Member mainMember;

    private void Start()
    {
        members = transform.GetComponentsInChildren<Member>();
        
        onInit.Invoke(members);
        
        for (var i = 0; i < members.Length; i++)
        {
            members[i].index = i;
            members[i].isMain = false;
        }
        SelectOneUnit(0);
    }

    public void SelectMultipleUnit(int[] indexes)
    {
        foreach (var index in indexes)
        {
            if(selectedUnits.Contains(members[index])) continue;
            
            selectedUnits.Add(members[index]);
            members[index].controller.VisualizeSelected(true);
        }

        if (selectedUnits.Count > 0 && mainMember != selectedUnits[0])
        {
            mainMember = selectedUnits[0];
            mainMember.isMain = true;
            for (int i = 1; i < members.Length; i++) { members[i].isMain = false; }
            onMainMemberChanges.Invoke(mainMember);
        }
    }

    public void SelectOneUnit(int index)
    {
        if(selectedUnits.Contains(members[index]) && selectedUnits.Count == 1) return;
        
        ClearAllSelected();
        
        selectedUnits.Add(members[index]);

        if (mainMember) mainMember.isMain = false;
        
        mainMember = selectedUnits[0];
        mainMember.isMain = true;
        mainMember.controller.VisualizeSelected(true);
        onMainMemberChanges.Invoke(mainMember);
    }

    public void ClearAllSelected()
    {
        foreach (var selectedUnit in selectedUnits)
        {
            selectedUnit.controller.VisualizeSelected(false);
        }
        selectedUnits.Clear();
    }
}