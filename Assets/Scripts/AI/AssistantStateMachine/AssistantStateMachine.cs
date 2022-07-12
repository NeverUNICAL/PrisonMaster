using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;

[RequireComponent(typeof(Assistant))]
public class AssistantStateMachine : MonoBehaviour
{
    [SerializeField] private AssistantState _firstState;

    private int _capacity;
    private AssistantState _currentState;
    private StackView _producerTarget;
    private StackView _stackView;
    private StackView _consumerTarget;

    public AssistantState CurrentState => _currentState;

    private void Start()
    {
        _producerTarget = GetComponent<Assistant>().ProducerTarget;
        _consumerTarget = GetComponent<Assistant>().ConsumerTarget;
        _capacity = GetComponent<Assistant>().Capacity;
        _stackView = GetComponentInChildren<StackView>();
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }

    private void Reset(AssistantState startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_producerTarget, _stackView, _capacity, _consumerTarget);
    }

    private void Transit(AssistantState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_producerTarget, _stackView, _capacity, _consumerTarget);
    }
}
