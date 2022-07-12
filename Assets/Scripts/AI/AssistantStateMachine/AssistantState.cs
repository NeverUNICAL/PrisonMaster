using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;

public abstract class AssistantState : MonoBehaviour
{
    [SerializeField] private List<AssistantTransition> _transitions;

    protected int Capacity { get; set; }
    protected StackView StackViewTarget { get; set; }
    protected StackView ProducerTarget { get; set; }
    protected StackView ConsumerTarget { get; set; }

    public void Enter(StackView producerTarget, StackView stackView, int capacity, StackView consumerTarget)
    {
        if (enabled == false)
        {
            ProducerTarget = producerTarget;
            StackViewTarget = stackView;
            Capacity = capacity;
            ConsumerTarget = consumerTarget;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(ProducerTarget, StackViewTarget, Capacity, ConsumerTarget);
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
        }

        return null;
    }
}
