using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Info")]
public class MemberInfo : ScriptableObject
{
    public string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    
    public string unitName;
    [TextArea] public string unitDescription;
    public Sprite portrait;
    public SkillBase[] skills;
    public AnimationModel animationModel;
}