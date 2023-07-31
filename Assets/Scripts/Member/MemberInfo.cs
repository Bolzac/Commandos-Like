using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Info")]
public class MemberInfo : ScriptableObject
{
    public string unitName;
    [TextArea] public string unitDescription;
    public Sprite portrait;
    public SkillBase[] skills;
    public AnimationModel animationModel;
}