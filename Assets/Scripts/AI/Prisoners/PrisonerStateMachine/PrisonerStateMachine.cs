using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerStateMachine : MonoBehaviour
{
    [SerializeField] private PrisonerState _firstState;

    private PrisonerState _currentState;
    private PrisonerMap2 _prisoner;

    public PrisonerState CurrentState => _currentState;

    private void Awake()
    {
        _prisoner = GetComponent<PrisonerMap2>();
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

    private void Reset(PrisonerState startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_prisoner);
    }

    private void Transit(PrisonerState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_prisoner);
    }
}
