using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;

public abstract class QueueHandler : MonoBehaviour
{
    [SerializeField]private float _spawnTimeOut =1f;
    [SerializeField]private float _sendTimeOut =1f;
    
    [SerializeField]protected GameObject _firstPoint;
    [SerializeField]protected int _startPoolSize = 5;
    [SerializeField]protected Vector3 _offsetPos;
    
    [Header("Pools Settings")]
    [SerializeField]protected List<QueueContainer> _queues;
    
    [Header("Queue Container Settings")]
    [SerializeField] protected Shop _shop;

    [SerializeField] protected bool _isSuitQueue;
    
    [Header("Distributor Settings")]
    [SerializeField] protected GameObject _exit;
    [SerializeField] protected float _tryToSendToExitCooldown;

    protected WaitForSeconds _waitToTrySendToExit;
    protected WaitForSeconds _waitForSpawnTimeOut;
    protected WaitForSeconds _waitForSendTimeOut;
    protected List<PrisonMover> _prisonerList;
    protected List<QueueContainer> _sortedList;
    
    private bool _isShopNotNull;

    public int PoolSize => _startPoolSize;
    public List<PrisonMover> PrisonerQueueList => _prisonerList;

    private void Awake()
    {
        _waitToTrySendToExit = new WaitForSeconds(_tryToSendToExitCooldown);
        _isShopNotNull = _shop != null;
        _waitForSpawnTimeOut = new WaitForSeconds(_spawnTimeOut);
        _waitForSendTimeOut = new WaitForSeconds(_sendTimeOut);
        _prisonerList = new List<PrisonMover>();
    }

    public void ListSort()
    {
        if(_prisonerList.Count == 1)
        {
            if(_isShopNotNull)
             _prisonerList[0].SetTarget(_firstPoint,Vector3.zero,_shop.gameObject.transform);
            else
                _prisonerList[0].SetTarget(_firstPoint,Vector3.zero);
                
        }
        
        for (int i = 0; i <_prisonerList.Count; i++)
        {
            _prisonerList[i].ChangePriority(i+1);
            
            if((i+1)<_prisonerList.Count)
            {
                _prisonerList[i+1].SetTarget(_prisonerList[i].gameObject,_offsetPos);
            }
            
            // _prisonerList[i].GetComponentInChildren<DebugViewer>().SetID(i+1);
        }
    }
    
    protected  void DestroyFirst()
    {
        Destroy(_prisonerList[0]);
        ExtractFirst();
    }
    
    protected void ExtractFirst()
    {
        _prisonerList.RemoveAt(0);
        ListSort();
        
        if(_prisonerList.Count>0)
        {
            if(_isShopNotNull)
                _prisonerList[0].SetTarget(_firstPoint,Vector3.zero,_shop.gameObject.transform);
            else
                _prisonerList[0].SetTarget(_firstPoint,Vector3.zero);
        }
    }

    protected void GenerateList()
    {
        _prisonerList= new List<PrisonMover>();
    }

    protected IEnumerator SendToQueue(List<QueueContainer> queues)
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
                if(_isSuitQueue)
                    _prisonerList[0].EnableSuit();
                        
                targetQueue.PrisonerQueueList.Add(_prisonerList[0]);
                //_prisonerList[0].ChangeAngryAnimation(false);
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
        _prisonerList[0].SetTarget(_exit,Vector3.zero);
        //_prisonerList[0].ChangeAngryAnimation(false);
        ExtractFirst();
        
        if(_shop != null)
         _shop.Sale();
    }
}
