using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Unit/Info")]
public class MemberInfo : ScriptableObject
{
    public string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    
    [Header("Personal Information")]
    public string memberName;
    [TextArea] public string memberDescription;
    public Sprite portrait;
    
    [Header("Abilities")]
    public SkillBase[] skills;
    
    [Header("Animation")] [Tooltip("It stands for animation names. We are saving member's animation and its affect animation")]
    public AnimationModel animationModel;
}