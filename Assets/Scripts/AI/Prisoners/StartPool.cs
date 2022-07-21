using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class StartPool : QueueHandler
{
    [SerializeField]private List<QueueContainer> _queues;
    [SerializeField]private PrisonMover _prisoner;
    [SerializeField]private Transform _spawnPoint;
    [SerializeField]private Transform _parentPrisoners;

    private PrisonMover[] _prisoners;
   
    private void Start()
    {
        GenerateList();
        StartCoroutine(Spawn());
        StartCoroutine(Send());
    }

    private void ListUpdate()
    {
        _prisonerList.Clear();
        _prisoners = FindObjectsOfType<PrisonMover>();
        
        foreach(PrisonMover prisoner in _prisoners)
        {
            _prisonerList.Add(prisoner);
        }
    }
    
    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return _waitForSpawnTimeOut;
            
            if(_prisonerList.Count<_startPoolSize)
            { 
                _prisonerList.Add(Instantiate(_prisoner, _spawnPoint.position, _spawnPoint.rotation, _parentPrisoners));
             ListSort();
            _prisonerList[0].SetTarget(_firstPoint,Vector3.zero);
            }
        }
    }
    
     private IEnumerator Send()
    { 
        while (true) 
        { 
            yield return _waitForSendTimeOut;

            List<QueueContainer> sortedList = _queues.OrderBy(queue => queue.PrisonerQueueList.Count).ToList();
            sortedList = sortedList.SkipWhile(queue => queue.PoolSize < 1).ToList();

            if(sortedList[0].PrisonerQueueList.Count<sortedList[0].PoolSize)
            { 
                if (_prisonerList.Count > 0) 
                {
                    sortedList[0].PrisonerQueueList.Add(_prisonerList[0]);
                    sortedList[0].ListSort();
                    ExtractFirst(); 
                } 
            } 
        } 
    }
}
