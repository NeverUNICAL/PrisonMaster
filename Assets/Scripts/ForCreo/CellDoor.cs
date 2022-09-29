using System.Collections;
using System.Collections.Generic;
using Agava.IdleGame;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshObstacle))]
public class CellDoor : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Vector3 _defaultPosition;
    [SerializeField] private Trigger _trigger;
    
    private NavMeshObstacle _navMeshObstacle;
    private Cell _cell;
    private bool _isStayed = false;
    private bool _isOpened = false;
    private Coroutine _checkPrisonerSittingStateInJob;

    public bool IsOpened => _isOpened;

    public event UnityAction Opened;
    public event UnityAction Closed;

    private void Awake()
    {
        _cell = GetComponentInParent<Cell>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    private void OnEnable()
    {
        _cell.PrisonerSendToPool += OnSendToPool;
        _trigger.Enter += OnPlayerEnterInCell;
        _trigger.Exit += OnPlayerExitOutCell;
        _cell.DoorButtonReached += OnReached;
        _cell.DoorButtonStay += OnStay;
        _cell.DoorButtonExit += OnExit;
    }

    private void OnDisable()
    {
        _cell.PrisonerSendToPool -= OnSendToPool;
        _trigger.Enter -= OnPlayerEnterInCell;
        _trigger.Exit -= OnPlayerExitOutCell;
        _cell.DoorButtonReached -= OnReached;
        _cell.DoorButtonStay -= OnStay;
        _cell.DoorButtonExit -= OnExit;
    }

    private void OnReached()
    {
        if (_isOpened == false)
            TryOpen();
        else
            TryClose();
    }

    private void OnStay()
    {
        _isStayed = true;   
    }

    private void OnPlayerEnterInCell()
    {
        _navMeshObstacle.enabled = false;
    }

    private void OnPlayerExitOutCell()
    {
        _navMeshObstacle.enabled = true;
    }

    private void OnExit()
    {
        _isStayed = false;

        if (_checkPrisonerSittingStateInJob != null)
            StopCoroutine(_checkPrisonerSittingStateInJob);

        if (_cell.IsPrisonerInCell == false)
            TryClose();
    }

    private void OnSendToPool()
    {
        if (_isStayed == false)
            TryClose();
    }

    private void TryOpen()
    {
        if (_cell.Prisoners.Count < 1)
            Open();
        else
            _checkPrisonerSittingStateInJob = StartCoroutine(CheckPrisonerSittingState(_cell.Prisoners[0]));
    }

    private void Open()
    {
        Move(_targetPosition, _duration, Opened);
        _isOpened = true;
    }

    private void TryClose()
    {
        if (_cell.Prisoners.Count < 1)
        {
            Close();
        }
        else
            StartCoroutine(CheckPrisonerMoveState(_cell.Prisoners[0]));
    }

    private void Close()
    {
        Move(_defaultPosition, _duration, Closed);
        _isOpened = false;
    }

    private void Move(Vector3 target, float duration, UnityAction action)
    {
        transform.DOLocalMove(target, duration).OnComplete(() => action?.Invoke());
    }

    private IEnumerator CheckPrisonerSittingState(PrisonerMover prisonerMover)
    {
        yield return new WaitWhile(() => prisonerMover.IsSittingInCell);

        Open();
    }

    private IEnumerator CheckPrisonerMoveState(PrisonerMover prisonerMover)
    {
        yield return new WaitWhile(() => prisonerMover.PathEnded() == false);

        Close();
    }
}
