using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public SelectionBoxHandler selectionBoxHandler;
    public CharacterPanelHandler characterPanelHandler;
    public SkillPanelHandler skillPanelHandler;

    private void Awake()
    {
        instance = this;
    }
}
