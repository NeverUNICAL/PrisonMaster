using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrisonerTransition : MonoBehaviour
{
    [SerializeField] private PrisonerState _targetState;

    protected StackView ConsumerTarget { get; private set; }
    public PrisonerState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    public void Init(StackView consumerTarget)
    {
        ConsumerTarget = consumerTarget;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
