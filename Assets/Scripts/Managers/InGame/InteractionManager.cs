using UnityEngine;
public class InteractionManager : MonoBehaviour
{
    private void Start()
    {
        Interactable[] interactables = FindObjectsOfType<Interactable>();

        foreach (Interactable interactable in interactables)
        {
            interactable.OnInteract += HandleInteraction;
        }
    }
        
    private void HandleInteraction(Interactable interactable)
    {
        TeamManagement.Instance.selectedUnits[0].controller.StartInteraction(interactable);
    }
}