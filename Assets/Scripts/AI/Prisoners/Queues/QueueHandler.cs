using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class QueueHandler : MonoBehaviour
{
    [SerializeField] private float _spawnTimeOut = 1f;
    [SerializeField] private float _sendTimeOut = 1f;
    [SerializeField] protected GameObject _firstPoint;
    [SerializeField] protected int _startPoolSize = 5;
    [SerializeField] protected Vector3 _offsetPos;
    [SerializeField] protected bool _isSuitQueue;

    [Header("Pools Settings")]
    [SerializeField] private int _countForTutor;
    [SerializeField] protected List<QueueHandler> _queues;

    [Header("Queue Container Settings")]
    [SerializeField] protected Shop _shop;
    [SerializeField] protected Store _store;
    [SerializeField] protected Cell _cellDoor;

    [Header("Shower Settings")]
    [SerializeField] protected bool _isInShowerQueue;

    [Header("Distributor Settings")]
    [SerializeField] protected GameObject _exit;
    [SerializeField] protected float _tryToSendToExitCooldown;

    protected WaitForSeconds _waitToTrySendToExit;
    protected WaitForSeconds _waitForSpawnTimeOut;
    protected WaitForSeconds _waitForSendTimeOut;
    protected List<PrisonerMover> _prisonerList;
    protected List<QueueHandler> _sortedList;

    private int _counter;

    private bool _isShopNotNull;
    private bool _isStoreNotNull;
    private bool _isCellNotNull;

    public int PoolSize => _startPoolSize;
    public List<PrisonerMover> PrisonerQueueList => _prisonerList;

    public event UnityAction PoolPrisonerAdded;

    private void Awake()
    {
        _waitToTrySendToExit = new WaitForSeconds(_tryToSendToExitCooldown);
        _waitForSpawnTimeOut = new WaitForSeconds(_spawnTimeOut);
        _waitForSendTimeOut = new WaitForSeconds(_sendTimeOut);
        _prisonerList = new List<PrisonerMover>();
        _isShopNotNull = _shop != null;
        _isStoreNotNull = _store != null;
        _isCellNotNull = _cellDoor != null;
    }

    public void ListSort()
    {
        if (_prisonerList.Count == 1)
        {
            if (_isShopNotNull)
                _prisonerList[0].SetTarget(_firstPoint, Vector3.zero, _shop.gameObject.transform);
            else if (_isCellNotNull)
                StartCoroutine(CheckCellDoorOpened());
            else
                _prisonerList[0].SetTarget(_firstPoint, Vector3.zero);
        }

        for (int i = 0; i < _prisonerList.Count; i++)
        {
            _prisonerList[i].ChangePriority(i + 1);

            if ((i + 1) < _prisonerList.Count)
            {
                _prisonerList[i + 1].SetTarget(_prisonerList[i].gameObject, _offsetPos);
            }
        }
    }

    public bool CheckForShopBuyed()
    {
        if ((_isShopNotNull && _shop.gameObject.activeInHierarchy) || (_isStoreNotNull && _store.gameObject.activeInHierarchy) || (_isCellNotNull && _cellDoor.gameObject.activeInHierarchy))
            return true;

        return false;
    }

    protected void DestroyFirst()
    {
        Destroy(_prisonerList[0]);
        ExtractFirst();
    }

    protected void ExtractFirst()
    {
        _prisonerList.RemoveAt(0);
       

        if (_prisonerList.Count > 0)
        {
            if (_isShopNotNull)
                _prisonerList[0].SetTarget(_firstPoint, Vector3.zero, _shop.gameObject.transform);
            else if (_isStoreNotNull)
                _prisonerList[0].SetTarget(_firstPoint, Vector3.zero, _store.gameObject.transform);
            else if (_isCellNotNull)
                StartCoroutine(CheckCellDoorOpened());
            else
                _prisonerList[0].SetTarget(_firstPoint, Vector3.zero);
        }
    }

    protected void GenerateList()
    {
        _prisonerList = new List<PrisonerMover>();
    }

    protected IEnumerator SendToQueue(List<QueueHandler> queues)
    {
        while (true)
        {
            yield return _waitForSendTimeOut;

            if (_queues[0].CheckForShopBuyed())
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

    protected bool SendToPool(QueueHandler targetQueue)
    {
        if (targetQueue.PrisonerQueueList.Count < targetQueue.PoolSize)
        {
            if (_prisonerList.Count > 0 && targetQueue._queues.Count > 0)
            {
                targetQueue.ChangeCounter();
                
                if (_isSuitQueue)
                    _prisonerList[0].EnableSuit();
                else if (_isInShowerQueue)
                    _prisonerList[0].EnableWashedSuit();

                targetQueue.PrisonerQueueList.Add(_prisonerList[0]);
                _prisonerList[0].ChangeAngryAnimation(false);
                targetQueue.ListSort();
                ExtractFirst();
                return true;
            }

            if (_prisonerList.Count > 0)
            {
                SendToExit();
            }
        }

        return false;
    }

    protected void SendToExit()
    {
        _prisonerList[0].SetTarget(_exit, Vector3.zero);
        _prisonerList[0].ChangeAngryAnimation(false);
        _prisonerList[0].EnableWashedSuit();
        ExtractFirst();

        if (_shop != null)
            _shop.Sale();
        else if (_store != null)
            _store.Sale();
        else if (_cellDoor != null)
            _cellDoor.Sale();
    }

    private void ChangeCounter()
    {
        _counter++;
        
        if (_counter >= _countForTutor)
        {
            PoolPrisonerAdded?.Invoke();
        }
    }

    private IEnumerator CheckCellDoorOpened()
    {
        yield return new WaitWhile(() => _cellDoor.CellDoor.IsOpened == false);

        _prisonerList[0].SetTarget(_firstPoint, Vector3.zero, _cellDoor.gameObject.transform);
    }
}
