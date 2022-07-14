using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrisonerTransition : MonoBehaviour
{
    [SerializeField] private PrisonerState _targetState;
    [SerializeField] private PrisonerState _alternativeTargetState;

    protected Prisoner Prisoner { get; private set; }
    protected StackView[] ConsumerTargets { get; private set; }
    protected PositionHandler[] TargetPositions { get; private set; }
    protected PositionHandler CurrentTargetPosition { get; private set; }
    protected ObjectTransferZone[] TransferZones { get; private set; }
    protected ObjectTransferZone CurrentTransferZone { get; private set; }
    public PrisonerState TargetState => _targetState;
    public PrisonerState AlternativeTargetState => _alternativeTargetState;
    public bool NeedTransit { get; protected set; }
    public bool NeedAlternativeTransit { get; protected set; }

    public void Init(StackView[] consumerTargets, PositionHandler[] targetPositions, ObjectTransferZone[] transferZones, Prisoner prisoner)
    {
        ConsumerTargets = consumerTargets;
        TargetPositions = targetPositions;
        TransferZones = transferZones;
        Prisoner = prisoner;
    }

    private void OnEnable()
    {
        NeedTransit = false;
        NeedAlternativeTransit = false;
    }
}
