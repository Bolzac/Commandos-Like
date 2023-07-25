using UnityEngine;

public class UnitModel : MonoBehaviour
{
    public UnitInfo info;
    public SkillBase readySkill;
    
    public bool isRunning;
    public bool isCrouching;
    public bool isInteractedWithSomething;
    public bool isSpeaking;
    public GameObject selection;

    public Transform soundSource;
    public float runningNoiseRadius;
}