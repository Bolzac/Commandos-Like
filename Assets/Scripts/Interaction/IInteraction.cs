using UnityEngine;

public abstract class IInteraction : MonoBehaviour
{
    public float distanceThreshold;

    public abstract void Interaction(Member member);
}