using UnityEngine.EventSystems;

public class SaveButton : BaseButton
{
    public override void OnSelect(BaseEventData eventData)
    {
        DataPersistenceManager.Instance.SaveGame();
    }
}