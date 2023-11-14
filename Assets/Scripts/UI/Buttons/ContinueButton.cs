using UnityEngine;
using UnityEngine.EventSystems;

public class ContinueButton : BaseButton
{
    private void OnEnable()
    {
        targetButton.interactable = true;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Open new game");
        //Create a new file for game data
        
        //GameManager.Instance.levelManager.LoadScene("Demo");
        //targetButton.interactable = false;
    }
}