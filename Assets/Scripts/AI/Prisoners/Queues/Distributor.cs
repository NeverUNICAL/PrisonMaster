using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Agava.IdleGame;
using UnityEngine;

public class Distributor : QueueHandler
{
    [Header("Cells Distributor Settings")]
    [SerializeField] private bool _isCellsDistributor;
    [SerializeField] private Trigger _trigger;

    private bool _inTrigger;
    
    private void Start()
    {
        if (_queues.Count > 0)
        {
           // StartCoroutine(TrySendToExit());
            if (_isCellsDistributor)
                StartCoroutine(SendToCellQueue(_queues));
            else
                StartCoroutine(SendToQueue(_queues));
        }
    }

    private void OnEnable()
    {
        if (_isCellsDistributor)
        {
            _trigger.Enter += OnEnter;
            _trigger.Exit += OnExit;
        }
    }

    private void OnDisable()
    {
        if (_isCellsDistributor)
        {
            _trigger.Enter -= OnEnter;
            _trigger.Exit -= OnExit;
        }

    }

    private IEnumerator TrySendToExit()
    {
        while (true)
        {
            if (_prisonerList.Count == _startPoolSize)
            {
                SendToExit();
            }

            yield return _waitToTrySendToExit;
        }
    }
    
    protected IEnumerator SendToCellQueue(List<QueueHandler> queues)
    {
        while (true)
        {
            yield return _waitForSendTimeOut;

            if (_queues[0].CheckForShopBuyed() && _inTrigger)
            {
                _sortedList = queues.OrderBy(queue => queue.PrisonerQueueList.Count).ToList();
                _sortedList = _sortedList.SkipWhile(queue => queue.PoolSize < 1 || queue.CheckForShopBuyed() == false)
                    .ToList();
                
                if (_sortedList[0].PrisonerQueueList.Count < _sortedList[0].PoolSize &&
                    _sortedList[0].CheckForShopBuyed())
                {
                    if (_prisonerList.Count > 0)
                    {
                        _sortedList[0].PrisonerQueueList.Add(_prisonerList[0]);
                        _sortedList[0].ListSort();
                        ExtractFirst();
                    }
                }
            }
        }
    }

    private void OnEnter()
    {
        _inTrigger = true;
    }

    private void OnExit()
    {
        _inTrigger = false;
    }
}
