using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Unit/Info")]
public class UnitInfo : ScriptableObject
{
    public string unitName;
    [TextArea] public string unitDescription;
    public Sprite portrait;
}