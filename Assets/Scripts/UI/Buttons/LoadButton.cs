using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadButton : BaseButton
{
    public override void OnSelect(BaseEventData eventData)
    {
        //DataPersistenceManager.instance.LoadGame();
        GameManager.Instance.levelManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}