using UnityEngine;
using UnityEngine.Serialization;

public abstract class IInteraction : MonoBehaviour
{
    public Transform animationTransform;
    public abstract void Interaction();
}