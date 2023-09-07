using UnityEngine;
public class InteractionManager : MonoBehaviour
{
    private void Awake()
    {
        foreach (Transform interactable in transform)
        {
            interactable.GetComponent<Interactable>().OnInteract += HandleInteraction;
        }
    }
        
    private void HandleInteraction(Interactable interactable)
    {
        TeamManagement.Instance.selectedUnits[0].controller.StartInteraction(interactable);
    }
}