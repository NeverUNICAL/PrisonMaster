using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;

public abstract class AssistantState : MonoBehaviour
{
    [SerializeField] private List<AssistantTransition> _transitions;

    protected int Capacity { get; set; }
    protected Assistant Assistant { get; set; }

    public void Enter(Assistant assistant)
    {
        if (enabled == false)
        {
            Assistant = assistant;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Assistant);
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

    public AssistantState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
            if (transition.NeedAlternativeTransit)
                return transition.TargetAlternativeState;
        }

        return null;
    }
}
