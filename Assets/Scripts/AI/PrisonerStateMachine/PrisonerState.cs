using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrisonerState : MonoBehaviour
{
    [SerializeField] private List<PrisonerTransition> _transitions;

    protected StackView[] ConsumerTargets { get; set; }
    protected PositionHandler[] TargetPositions { get; set; }
    protected ObjectTransferZone[] TransferZones { get; set; }
    protected Prisoner Prisoner { get; set; }

    public void Enter(StackView[] consumerTargets, PositionHandler[] positionHandlers, ObjectTransferZone[] transferZone, Prisoner prisoner)
    {
        if (enabled == false)
        {
            ConsumerTargets = consumerTargets;
            TargetPositions = positionHandlers;
            TransferZones = transferZone;
            Prisoner = prisoner;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(ConsumerTargets, TargetPositions, TransferZones, Prisoner);
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public PrisonerState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
            else if (transition.NeedAlternativeTransit)
                return transition.AlternativeTargetState;
        }

        return null;
    }
}
