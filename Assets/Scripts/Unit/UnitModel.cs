using UnityEngine;

public class UnitModel : MonoBehaviour
{
    public UnitInfo info;
    
    public bool isRunning;
    public bool isCrouching;
    public GameObject selection;

    public Transform soundSource;
    public float runningNoiseRadius;
}