using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIUnit : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent onFocusOnUnit;
    private Coroutine _coroutine;
    private int clickCount;
    public void OnPointerClick(PointerEventData eventData)
    {
        clickCount++;
        switch (clickCount)
        {
            case 1:
                _coroutine = StartCoroutine(ResetClickCount());
                break;
            case 2:
                if(_coroutine != null) StopCoroutine(_coroutine);
                onFocusOnUnit?.Invoke();
                clickCount = 0;
                break;
        }
    }

    private IEnumerator ResetClickCount()
    {
        yield return new WaitForSeconds(0.5f);
        clickCount = 0;
    }
}