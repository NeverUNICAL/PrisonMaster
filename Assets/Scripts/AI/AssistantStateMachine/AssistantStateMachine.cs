using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;

public class AssistantStateMachine : MonoBehaviour
{
    [SerializeField] private AssistantState _firstState;

   
    private AssistantState _currentState;
    private StackingAssistent _assistant;

    public AssistantState CurrentState => _currentState;

    private void Awake()
    {
        _assistant = GetComponent<StackingAssistent>();
    }

    private void Start()
    {
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
            _currentState.Enter(_assistant);
    }

    private void Transit(AssistantState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_assistant);
    }
}
