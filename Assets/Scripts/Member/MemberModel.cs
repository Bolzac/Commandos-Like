using UnityEngine;

public class MemberModel : MonoBehaviour
{
    public MemberInfo info;
    public SkillBase readySkill;
    
    public bool isRunning;
    public bool isCrouching;
    public bool isInteractedWithSomething;
    public GameObject selection;

    public Transform soundSource;
    public float runningNoiseRadius;
}