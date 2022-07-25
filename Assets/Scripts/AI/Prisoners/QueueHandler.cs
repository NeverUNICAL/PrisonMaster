using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class QueueHandler : MonoBehaviour
{
    [SerializeField]private float _spawnTimeOut =1f;
    [SerializeField]private float _sendTimeOut =1f;
    
    [SerializeField]protected GameObject _firstPoint;
    [SerializeField]protected int _startPoolSize = 5;
    [SerializeField]protected Vector3 _offsetPos;

    protected WaitForSeconds _waitForSpawnTimeOut;
    protected WaitForSeconds _waitForSendTimeOut;
    protected List<PrisonMover> _prisonerList;
    protected List<QueueHandler> _sortedList;
    
    public int PoolSize => _startPoolSize;
    public List<PrisonMover> PrisonerQueueList => _prisonerList;

    private void Awake()
    {
        _waitForSpawnTimeOut = new WaitForSeconds(_spawnTimeOut);
        _waitForSendTimeOut = new WaitForSeconds(_sendTimeOut);
        _prisonerList = new List<PrisonMover>();
    }

    public void ListSort()
    {
        if(_prisonerList.Count == 1)
        {
            _prisonerList[0].SetTarget(_firstPoint,Vector3.zero);
        }
        
        for (int i = 0; i <_prisonerList.Count; i++)
        {
            _prisonerList[i].ChangePriority(i+1);
            
            if((i+1)<_prisonerList.Count)
            {
                _prisonerList[i+1].SetTarget(_prisonerList[i].gameObject,_offsetPos);
            }
            
            _prisonerList[i].GetComponentInChildren<DebugViewer>().SetID(i+1);
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
            _prisonerList[0].SetTarget(_firstPoint,Vector3.zero);
        }
    }

    protected void GenerateList()
    {
        _prisonerList= new List<PrisonMover>();
    }

    protected IEnumerator SendToQueue(List<QueueHandler> queues)
    {
        while (true)
        {
            yield return _waitForSendTimeOut;

            _sortedList = queues.OrderBy(queue => queue.PrisonerQueueList.Count).ToList();
            _sortedList = _sortedList.SkipWhile(queue => queue.PoolSize < 1).ToList();

            if (_sortedList[0].PrisonerQueueList.Count < _sortedList[0].PoolSize)
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

    protected bool SendToPool(QueueHandler targetQueue)
    {
        if (targetQueue.PrisonerQueueList.Count < targetQueue.PoolSize)
        {
                if (_prisonerList.Count > 0)
                {
                    targetQueue.PrisonerQueueList.Add(_prisonerList[0]);
                    targetQueue.ListSort();
                    ExtractFirst();
                    return true;
                }
        }

        return false;
    }
}
