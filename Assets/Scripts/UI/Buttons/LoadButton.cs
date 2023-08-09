using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadButton : BaseButton
{
    public override void OnSelect(BaseEventData eventData)
    {
        DataPersistenceManager.Instance.LoadGame();
        GameManager.Instance.levelManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}