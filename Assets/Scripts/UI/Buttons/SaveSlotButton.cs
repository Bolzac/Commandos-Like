using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveSlotButton : BaseButton
{
    [SerializeField] private string profileId;

    [Header("Content")] 
    [SerializeField] private GameObject noData;
    [SerializeField] private GameObject hasData;
    [SerializeField] private TextMeshProUGUI date;

    public void SetData(GameData data)
    {
        if (data == null)
        {
            noData.SetActive(true);
            hasData.SetActive(false);
        }
        else
        {
            noData.SetActive(false);
            hasData.SetActive(true);
            //date.text = "Created at: " + data.createTime.ToString(CultureInfo.InvariantCulture);
        }
    }

    public string GetProfileId()
    {
        return profileId;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        targetButton.interactable = false;
        /*
        DataPersistenceManager.Instance.ChangeSelectedProfileId(profileId);
        if(hasData.activeSelf) DataPersistenceManager.Instance.LoadGame();
        else DataPersistenceManager.Instance.NewGame();
        */
        GameManager.Instance.levelManager.LoadScene("Demo");
    }
}
