using UnityEngine;
public class AbilityHolder : MonoBehaviour
{
    public IndicatorBase[] indicators;
    public int activeIndex;
    public SkillBase activeSkill;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (activeSkill && Input.GetKeyUp(KeyCode.Mouse1)) //Cancel skill
        {
            activeSkill = null;
            HidePreview(activeIndex);
        }
        
        for (var i = 0; i < indicators.Length; i++)
        {
            if (Input.GetKeyDown(indicators[i].ability.key))
            {
                if(activeSkill != indicators[i].ability) HidePreview(activeIndex);
                ShowPreview(i);
            }
        }
    }

    private void ShowPreview(int index)
    {
        TeamManagement.Instance.selectedUnits[0].model.isSkillEnable = true;
        activeIndex = index;
        activeSkill = indicators[index].ability;
        transform.GetChild(index).gameObject.SetActive(true);
    }
    
    
    private void HidePreview(int index)
    {
        TeamManagement.Instance.selectedUnits[0].model.isSkillEnable = false;
        transform.GetChild(index).gameObject.SetActive(false);
    }
}