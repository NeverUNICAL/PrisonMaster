using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;

public abstract class AssistantTransition : MonoBehaviour
{
    [SerializeField] private AssistantState _targetState;
    [SerializeField] private AssistantState _targetAlternativeState;

    protected StackingAssistent Assistant { get; private set; }
    protected int Capacity { get; private set; }
    protected StackView StackViewTarget { get; private set; }
    protected StackView ProducerTarget { get; private set; }
    protected StackView ConsumerTarget { get; private set; }
    public AssistantState TargetState => _targetState;
    public AssistantState TargetAlternativeState => _targetAlternativeState;
    public bool NeedTransit { get; protected set; }
    public bool NeedAlternativeTransit { get; protected set; }


    public void Init(StackingAssistent assistant)
    {
        Assistant = assistant;
    }

    private void OnEnable()
    {
        NeedTransit = false;
        NeedAlternativeTransit = false;
    }
}
