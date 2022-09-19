using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CellDoor : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Vector3 _defaultPosition;

    private Cell _cell;
    private bool _isOpened = false;

    public bool IsOpened => _isOpened;

    public event UnityAction Opened;
    public event UnityAction Closed;

    private void Awake()
    {
        _cell = GetComponentInParent<Cell>();
    }

    private void OnEnable()
    {
        _cell.DoorButtonReached += OnReached;
        _cell.DoorButtonExit += OnExit;
    }

    private void OnDisable()
    {
        _cell.DoorButtonReached -= OnReached;
        _cell.DoorButtonExit -= OnExit;
    }

    private void OnReached()
    {
        if (_isOpened == false)
            TryOpen();
        else
            Close();
    }

    private void OnExit()
    {
        Close();
    }

    public void TryOpen()
    {
        if (_cell.Prisoners.Count < 1)
            Open();
        else
            StartCoroutine(CheckPrisonerState(_cell.Prisoners[0]));
    }

    private void Open()
    {
        Move(_targetPosition, _duration, Opened);
        _isOpened = true;
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

    private IEnumerator CheckPrisonerState(PrisonerMover prisonerMover)
    {
        yield return new WaitWhile(() => prisonerMover.IsSittingInCell);

        Open();
    }
}
