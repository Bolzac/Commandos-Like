using UnityEngine;

public class UISkillSelection : MonoBehaviour
{
    public GameObject[] skillBlocks;

    private void ClearSkillBar()
    {
        foreach (var skillBlock in skillBlocks)
        {
            skillBlock.SetActive(false);
        }
    }
    public void OnSelectionUnitChanged(Unit unit)
    {
        ClearSkillBar();
        Transform child;
        for (var i = 0; i < unit.model.info.skills.Length; i++)
        {
            child = transform.GetChild(0).GetChild(i);
            child.GetComponent<UISkill>().SetSkillBlock(unit.model.info.skills[i]);
        }
    }
}