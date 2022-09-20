using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrisonerTransition : MonoBehaviour
{
    [SerializeField] private PrisonerState _targetState;

    protected PrisonerMap2 Prisoner { get; private set; }
    public PrisonerState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    public void Init(PrisonerMap2 prisoner)
    {
        Prisoner = prisoner;
    }

    protected void OnEnable()
    {
        NeedTransit = false;
    }

    public void DisableTransit()
    {
        NeedTransit = false;
    }
}
