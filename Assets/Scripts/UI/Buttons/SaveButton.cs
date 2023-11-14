using UnityEngine.EventSystems;

public class SaveButton : BaseButton
{
    public override void OnSelect(BaseEventData eventData)
    {
        //DataPersistenceManager.instance.SaveGame();
    }
}