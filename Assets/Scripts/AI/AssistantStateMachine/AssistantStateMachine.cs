using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;

[RequireComponent(typeof(Assistant))]
public class AssistantStateMachine : MonoBehaviour
{
    [SerializeField] private AssistantState _firstState;

    private float _speed;
    private int _capacity;
    private AssistantState _currentState;
    private Assistant _assistant;

    public AssistantState CurrentState => _currentState;

    private void Start()
    {
        _assistant = GetComponent<Assistant>();
        _capacity = GetComponent<Assistant>().Capacity;
        _speed = GetComponent<Assistant>().Speed;
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
