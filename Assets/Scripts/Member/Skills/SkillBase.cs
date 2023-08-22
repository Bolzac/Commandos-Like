using UnityEngine;
public abstract class SkillBase : ScriptableObject
{
    [Header("Skill Introduction")]
    public string skillName;
    [TextArea] public string skillDescription;
    public Sprite skillSprite;

    [Header("Skill Properties")]
    public float cooldown;
    
    public virtual void ReadySkill()
    {
        Debug.Log("ready skill");
    }

    public virtual void UseSkill()
    {
        Debug.Log("Use Skill");
    }
}