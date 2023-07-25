using UnityEngine;
public class SingleSelector
{
    private Coroutine _coroutine;
    
    public void SingleSelection(UnitManager unitManager)
    {
        var ray = unitManager.cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity,unitManager.teamModel.everything))
        {
            if (hit.transform.TryGetComponent(out Unit unit))
            {
                unitManager.SelectOneUnit(unit.index);
            }
            else
            {
                if(_coroutine != null) unitManager.StopCoroutine(_coroutine);
                if (hit.transform.CompareTag("Ground"))
                {
                    unitManager.teamController.AssignDestinations(hit.point);
                }
                else if(hit.transform.TryGetComponent(out IInteraction interact))
                {
                    _coroutine = unitManager.StartCoroutine(unitManager.selectedUnits[0].controller.Interaction(interact));
                }
            }
        }
    }
}