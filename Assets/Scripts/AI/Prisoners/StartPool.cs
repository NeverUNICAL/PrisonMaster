using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class StartPool : QueueHandler
{

    private Prisoner[] _prisoners;
    [SerializeField]private List<QueueContainer> _queues;
    [SerializeField]private GameObject _prisoner;
    [SerializeField]private Transform _spawnPoint;
    [SerializeField]private Transform _parentPrisoners;


    void Start()
    {
        GenerateList();
        StartCoroutine(Spawn());
        StartCoroutine(Send());
    }

    public void ListUpdate()
    {
        _prisonerList.Clear();
        _prisoners = GameObject.FindObjectsOfType<Prisoner>();
        Debug.Log(_prisoners[0]);

        foreach(Prisoner Prisoner in _prisoners)
        {
            _prisonerList.Add(Prisoner.gameObject);
        }

    }



    private  IEnumerator Spawn()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(_spawnTimeOut);
            
            if(_prisonerList.Count<_startPoolSize)
            {
            _prisonerList.Add(Instantiate(_prisoner, _spawnPoint.position, _spawnPoint.rotation, _parentPrisoners));
            ListSort();
            _prisonerList[0].GetComponent<PrisonMover>().SetTarget(_firstPoint,Vector3.zero);
            }
           
        }
    }


    
private IEnumerator Send()
    {
        while (true)
        {
            yield return new WaitForSeconds(_sendTimeOut);

            List<QueueContainer> sortedList = _queues.OrderBy(queue => queue.Count).ToList();

            if(sortedList[0].Count<sortedList[0].PoolSize)
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
