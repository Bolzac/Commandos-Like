using UnityEngine;

public class UnitModel : MonoBehaviour
{
    public UnitInfo info;
    
    public bool isRunning;
    public bool isCrouching;
    public Vector3 destination;
    public GameObject selection;

    public Transform soundSource;
    public float runningNoiseRadius;

    public float walkingSpeed;
    public float crouchSpeed;
    public float runningSpeed;

    public string unitLockedAnim;
    public string unitOpenAnim;
    public string unitCloseAnim;
}