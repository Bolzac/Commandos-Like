using UnityEngine.EventSystems;

public class ContinueButton : BaseButton
{
    private void OnEnable()
    {
        if (!DataPersistenceManager.Instance.HasGameData()) targetButton.interactable = false;
        else targetButton.interactable = true;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        DataPersistenceManager.Instance.LoadGame();
        GameManager.Instance.levelManager.LoadScene("Demo");
        targetButton.interactable = false;
    }
}