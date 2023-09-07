using System.Collections;
using UnityEngine;
public class SkillBase : ScriptableObject
{
    [Header("Skill Introduction")]
    public string skillName;
    [TextArea] public string skillDescription;
    public Sprite skillSprite;

    [Header("Skill Properties")] 
    public float cooldown;
    public float cooldownTimer;
    public float rangeDistance;
    public float noiseDistance;

    [Header("Trigger")]
    public KeyCode key;

    public virtual void ActivateSkill()
    {
        Debug.Log("Skill activated");
    }

    public IEnumerator StartCooldown()
    {
        while (cooldownTimer != 0)
        {
            yield return new WaitForSeconds(1);
            cooldownTimer -= 1;
        }

        cooldownTimer = cooldown;
    }
}