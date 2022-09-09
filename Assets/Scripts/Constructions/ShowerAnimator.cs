using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _inCoverAnimator;
    [SerializeField] private Animator _outCoverAnimator;
    [SerializeField] private ShowerQueueContainer _showerQueueContainer;
    private static readonly int IsOpened = Animator.StringToHash("IsOpened");

    private void OnEnable()
    {
        _showerQueueContainer.PrisonerIsIn += OnPrisonerIsIn;
        _showerQueueContainer.PrisonerWashEnded += OnPrisonerWashEnded;
    }

    private void OnPrisonerWashEnded()
    {
        _outCoverAnimator.SetBool(IsOpened,true);
        _inCoverAnimator.SetBool(IsOpened,true);
    }
    
    private void OnPrisonerIsIn()
    {
        _outCoverAnimator.SetBool(IsOpened,false);
        _inCoverAnimator.SetBool(IsOpened,false);
    }
}
