using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StartPool : MonoBehaviour
{
    private List<GameObject> _prisonerList;
    private Prisoner[] _prisoners;
    [SerializeField]private GameObject _prisoner;
    [SerializeField]private GameObject _firstPoint;
    [SerializeField]private Transform _spawnPoint;
    [SerializeField]private Transform _parentPrisoners;

    [SerializeField]private int _startPoolSize = 5;
    [SerializeField]private float _spawnTimeOut =1f;
    [SerializeField]private float _sendTimeOut =1f;
    void Start()
    {
        _prisonerList= new List<GameObject>();
        StartCoroutine(Spawn());
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

    public void ListSort()
    {
        for (int i = 0; i <_prisonerList.Count; i++)
        {
            _prisonerList[i].GetComponent<NavMeshAgent>().avoidancePriority = i+1;
            if((i+1)<_prisonerList.Count){
                _prisonerList[i+1].GetComponent<PrisonMover>().SetTarget(_prisonerList[i]);
            }
            _prisonerList[i].GetComponentInChildren<DebugViewer>().SetID(i+1);
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
            _prisonerList[0].GetComponent<PrisonMover>().SetTarget(_firstPoint);
            }
           
        }
    }
    
    private IEnumerator Send()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(_sendTimeOut);
            

           
        }
    }
    
    public  void Destroy()
    {
        Destroy(_prisonerList[0]);
        _prisonerList.RemoveAt(0);
        ListSort();
        _prisonerList[0].GetComponent<PrisonMover>().SetTarget(_firstPoint);
    }
}
