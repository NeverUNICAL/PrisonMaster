using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;

public abstract class AssistantTransition : MonoBehaviour
{
    [SerializeField] private AssistantState _targetState;

    protected int Capacity { get; private set; }
    protected StackView StackViewTarget { get; private set; }
    protected StackView ProducerTarget { get; private set; }
    protected StackView ConsumerTarget { get; private set; }
    public AssistantState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    public void Init(StackView producerTarget, StackView stackView, int capacity, StackView consumerTarget)
    {
        ProducerTarget = producerTarget;
        ConsumerTarget = consumerTarget;
        StackViewTarget = stackView;
        Capacity = capacity;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
