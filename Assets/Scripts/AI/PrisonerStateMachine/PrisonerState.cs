using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrisonerState : MonoBehaviour
{
    [SerializeField] private List<PrisonerTransition> _transitions;

    protected Prisoner Prisoner { get; set; }

    public void Enter(Prisoner prisoner)
    {
        if (enabled == false)
        {
            Prisoner = prisoner;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Prisoner);
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
