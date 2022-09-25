using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsControl : MonoBehaviour
{
    [SerializeField] private CellQueueContainer[] _cellsQueue;
    [SerializeField] private float _cooldawn = 5f;

    private CellQueueContainer _currentCellQueue;
    private CellQueueContainer _nextCellQueue;

    private Cell[] _cells;
    private bool _isMovingTargetCell = false;
    private Assistant _assistant;
    private int _counter;

    private void Awake()
    {
        _assistant = GetComponent<Assistant>();

        _cells = new Cell[_cellsQueue.Length];

        for (int i = 0; i < _cells.Length; i++)
            _cells[i] = _cellsQueue[i].Cell;
    }

    private void OnEnable()
    {
        for (int i = 0; i < _cellsQueue.Length; i++)
        {
            _cellsQueue[i].PrisonerEmptyed += OnPrisonerEmptyed;
            _cellsQueue[i].Cell.PrisonerSendToPool += OnPrisonerSendToPool;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _cellsQueue.Length; i++)
        {
            _cellsQueue[i].PrisonerEmptyed -= OnPrisonerEmptyed;
            _cellsQueue[i].Cell.PrisonerSendToPool -= OnPrisonerSendToPool;
        }
    }

    private void Start()
    {
        _isMovingTargetCell = true;
        StartCoroutine(CheckIdle());
    }

    private void OnPrisonerEmptyed(PrisonerMover prisonerMover, CellQueueContainer cellQueueContainer)
    {
        if (_currentCellQueue == null)
            _currentCellQueue = cellQueueContainer;
        else if (_currentCellQueue != cellQueueContainer)
            _nextCellQueue = cellQueueContainer;
        else
            CheckPrisonerInSitting();

        if (_currentCellQueue != null)
            _assistant.Move(_currentCellQueue.Cell.DoorButton.transform.position);
    }

    private void OnPrisonerSendToPool()
    {
        _isMovingTargetCell = false;
        _currentCellQueue = null;
        OnPrisonerEmptyed(null, _nextCellQueue);
    }

    private void CheckPrisonerInSitting()
    {
        for (int i = 0; i < _cellsQueue.Length; i++)
        {
            if (_cellsQueue[i].Cell.IsPrisonerInCell)
            {
                _currentCellQueue = _cellsQueue[i];
                return;
            }
        }
    }

    private IEnumerator CheckIdle()
    {
        while (true)
        {
            yield return new WaitForSeconds(_cooldawn);

            _counter = 0;
            if (_isMovingTargetCell == false)
            {
                for (int i = 0; i < _cells.Length; i++)
                {
                    if (_cells[i].gameObject.activeInHierarchy)
                        _counter++;
                }

                if (_currentCellQueue == null)
                {
                    int random = Random.Range(0, _counter);
                    _assistant.Move(_cellsQueue[random].Cell.DoorButton.transform.position);
                }
            }
        }
    }
}
