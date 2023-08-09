using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotMenu : MonoBehaviour
{
    public SaveSlotButton[] saveSlots;

    private void Awake()
    {
        saveSlots = GetComponentsInChildren<SaveSlotButton>();
    }

    private void OnEnable()
    {
        ActivateMenu();
    }

    private void ActivateMenu()
    {
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.Instance.GetAllProfilesGameData();

        foreach (var saveSlot in saveSlots)
        {
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out GameData profileData);
            saveSlot.SetData(profileData);
        }
    }
}
