using UnityEngine;
public abstract class SkillBase : ScriptableObject
{
    public string skillName;
    [TextArea] public string skillDescription;
    public Sprite skillSprite;

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