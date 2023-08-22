using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class UI_Screen : MonoBehaviour
{
    #region Variables

    [FormerlySerializedAs("mStartSelectable")] [Header("Main Properties")]
    public SelectableMember mStartSelectableMember;

    public UnityEvent onScreenStart;
    public UnityEvent onScreenClose;
    
    private Animator _animator;

    #endregion

    #region Main Methods

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if (mStartSelectableMember) EventSystem.current.SetSelectedGameObject(mStartSelectableMember.gameObject);
    }

    #endregion

    #region Helper Methods

    public virtual void StartScreen()
    {
        onScreenStart?.Invoke();
        gameObject.SetActive(true);
        //HandleAnimator("show");
    }

    public virtual void CloseScreen()
    {
        onScreenClose?.Invoke();
        gameObject.SetActive(false);
        //HandleAnimator("hide");
    }

    private void HandleAnimator(string aTrigger)
    {
        if(_animator) _animator.SetTrigger(aTrigger);
    }

    #endregion
}
