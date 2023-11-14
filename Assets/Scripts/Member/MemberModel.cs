using UnityEngine;
using UnityEngine.Serialization;

public class MemberModel : MonoBehaviour
{
    public MemberInfo info;

    public bool isRunning;
    public bool isCrouching;
    public bool isSkillEnable;
    public GameObject selectedVisual;
    public Transform follow;
}