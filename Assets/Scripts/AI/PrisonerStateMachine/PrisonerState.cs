using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrisonerState : MonoBehaviour
{
    [SerializeField] private List<PrisonerTransition> _transitions;

    protected ObjectConsumerZone ConsumerTarget { get; set; }

    public void Enter(ObjectConsumerZone consumerTarget)
    {
        if (enabled == false)
        {
            ConsumerTarget = consumerTarget;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(ConsumerTarget);
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
        }

        return null;
    }
}
