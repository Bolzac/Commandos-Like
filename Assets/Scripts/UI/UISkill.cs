using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class UISkill : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    private bool _isAssigned;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject descriptionBlock;
    
    [SerializeField] private float duration;
    private Coroutine _coroutine;
    
    private void Awake()
    {
        descriptionBlock = transform.GetChild(0).gameObject;
        image = GetComponent<Image>();
        descriptionText = descriptionBlock.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        titleText = descriptionBlock.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SetSkillBlock(SkillBase skill)
    {
        _isAssigned = true;
        image.sprite = skill.skillSprite;
        titleText.text = skill.skillName;
        descriptionText.text = skill.skillDescription;
    }

    public void ClearSkillBlock()
    {
        _isAssigned = false;
        image.sprite = null;
        titleText.text = "";
        descriptionText.text = "";
    }

    private IEnumerator ShowDescription()
    {
        yield return new WaitForSeconds(duration);
        descriptionBlock.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!_isAssigned) return;
        _coroutine = StartCoroutine(ShowDescription());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!_isAssigned) return;
        if(_coroutine != null)  StopCoroutine(_coroutine);
        descriptionBlock.SetActive(false);
    }
}