using UnityEngine;
using Image = UnityEngine.UI.Image;

public class UISelectionController : MonoBehaviour
{
    public UnitManager unitManager;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Transform child;
        for (var i = 0; i < unitManager.units.Length; i++)
        {
            child = transform.GetChild(i);
            child.gameObject.SetActive(true);
            child.GetComponent<Image>().sprite = unitManager.units[i].model.info.portrait;
        }
    }
}