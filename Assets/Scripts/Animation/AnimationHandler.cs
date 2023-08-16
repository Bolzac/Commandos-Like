using System;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator;
    private readonly int _speed = Animator.StringToHash("Speed");
    private readonly int _onAction = Animator.StringToHash("OnAction");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void PlayTargetAnim(string animName, float time)
    {
        animator.SetBool(_onAction,true);
        animator.CrossFadeInFixedTime(animName,time,0);
    }

    public void PlayUpperBodyAnim(string animName, float time)
    {
        animator.CrossFadeInFixedTime(animName,time,1);
    }

    public void SetPatrolBlend(float speed)
    {
        animator.SetFloat(_speed,speed);
    }

    public void SetActionOff()
    {
        animator.SetBool(_onAction,false);
    }

    private void OnAnimatorMove()
    {
        if (animator.rootPosition != transform.parent.position)
        {
            transform.parent.position = transform.position + animator.deltaPosition;
        }
    }
}