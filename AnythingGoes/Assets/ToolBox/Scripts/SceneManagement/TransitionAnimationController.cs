using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAnimationController : MonoBehaviour
{
    public Animator anim;
    
    public void TransitionLoad()
    {
        onOK?.Invoke();
    }
    Action onOK;
    internal void BeginTransition(Action onOk)
    {
        onOK = onOk;
        anim.ResetTrigger("EndTransition");
        anim.SetTrigger("Transition");
        
    }
    internal void TransitionMidPoint()
    {
        onOK?.Invoke();
    }
    internal void FinishTransition()
    {
        
        anim.ResetTrigger("Transition");
        anim.SetTrigger("EndTransition");
    }
}
