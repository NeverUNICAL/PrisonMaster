using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Prisoner))]
public class PrisonerStateMachine : MonoBehaviour
{
    [SerializeField] private PrisonerState _firstState;

    private Prisoner _prisoner;
    private PrisonerState _currentState;

    private bool _isFirstState = true;
    private bool _isZone1Complete = false;
    private bool _isZone2Complete = false;
    private bool _isZone3Complete = false;

    public ObjectTransferZone CurrentTransferZone { get; private set; }
    public PrisonerState CurrentState => _currentState;

    private void Start()
    {
        _prisoner = GetComponent<Prisoner>();

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
        {
            _currentState.Enter(_prisoner.ConsumerTargetsZone1, _prisoner.PositionHandlersZone1, _prisoner.TransferZones1, _prisoner);
        }
    }

    private void Transit(PrisonerState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
        {
            if (_isZone1Complete == false)
            {
                _currentState.Enter(_prisoner.ConsumerTargetsZone1, _prisoner.PositionHandlersZone1, _prisoner.TransferZones1, _prisoner);
                if (_isFirstState == false)
                    _isZone1Complete = true;
                else
                    _isFirstState = false;
            }
            else if (_isZone2Complete == false)
            {
                _currentState.Enter(_prisoner.ConsumerTargetsZone2, _prisoner.PositionHandlersZone2, _prisoner.TransferZones2, _prisoner);
                _isZone2Complete = true;
            }
            else if (_isZone3Complete == false)
            {
                _currentState.Enter(_prisoner.ConsumerTargetsZone3, _prisoner.PositionHandlersZone3, _prisoner.TransferZones3, _prisoner);
                _isZone3Complete = true;
            }
        }
    }
}
