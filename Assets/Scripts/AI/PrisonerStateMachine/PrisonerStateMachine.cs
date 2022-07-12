using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Prisoner))]
public class PrisonerStateMachine : MonoBehaviour
{
    [SerializeField] private PrisonerState _firstState;

    private PrisonerState _currentState;
    private StackView _consumerTarget;

    public PrisonerState CurrentState => _currentState;

    private void Start()
    {
        _consumerTarget = GetComponent<Assistant>().ConsumerTarget;
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

    private void Reset(PrisonerState startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_consumerTarget);
    }

    private void Transit(PrisonerState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_consumerTarget);
    }
}
