using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _inCoverAnimator;
    [SerializeField] private Animator _outCoverAnimator;
    [SerializeField] private float _delayCloseInCover;
    [SerializeField] private float _delayCloseOutCover;
}
