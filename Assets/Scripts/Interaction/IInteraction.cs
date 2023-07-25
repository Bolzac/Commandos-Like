using UnityEngine;

public abstract class IInteraction : MonoBehaviour
{
    public float distanceThreshold;
    public bool isSpeakable;

    public abstract void Interaction(Unit unit);
}