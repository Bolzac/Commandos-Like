using UnityEngine;
using UnityEngine.Serialization;

public class SelectionManager : MonoBehaviour
{
    [FormerlySerializedAs("worldSelection")] public DestinationSelection destinationSelection;
    public MultipleMemberSelection multipleMemberSelection;

    public void ObserveMouseBehaviour()
    {
        if (Input.GetMouseButtonDown(0))
        {
            multipleMemberSelection.SetStartPos(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            multipleMemberSelection.SetEndPos(Input.mousePosition);
            multipleMemberSelection.DrawVisual();
            multipleMemberSelection.DrawSelection();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (multipleMemberSelection.isDragging)
            {
                multipleMemberSelection.SelectMembers();
                multipleMemberSelection.SetStartPos(Vector2.zero);
                multipleMemberSelection.SetEndPos(Vector2.zero);
                multipleMemberSelection.DrawVisual();
            }
            else
            {
                destinationSelection.SelectDestination();
            }
        }
    }
}