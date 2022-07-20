using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class QueueHandler : MonoBehaviour
{
    protected List<GameObject> _prisonerList;
    public int Count => _prisonerList.Count;
    [SerializeField]protected GameObject _firstPoint;
    [SerializeField]protected int _startPoolSize = 5;
    [SerializeField]protected float _spawnTimeOut =1f;
    [SerializeField]protected float _sendTimeOut =1f;
    [SerializeField]protected Vector3 _offsetPos;
    public int PoolSize => _startPoolSize;
    public void ListSort()
    {
        if(_prisonerList.Count == 1)
        {
            _prisonerList[0].GetComponent<PrisonMover>().SetTarget(_firstPoint,Vector3.zero);
        }
        for (int i = 0; i <_prisonerList.Count; i++)
        {
            _prisonerList[i].GetComponent<NavMeshAgent>().avoidancePriority = i+1;
            if((i+1)<_prisonerList.Count){
                _prisonerList[i+1].GetComponent<PrisonMover>().SetTarget(_prisonerList[i],_offsetPos);
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
            _prisonerList[0].GetComponent<PrisonMover>().SetTarget(_firstPoint,Vector3.zero);
        }

    }
    protected void GenerateList()
    {
        _prisonerList= new List<GameObject>();
    }

}
