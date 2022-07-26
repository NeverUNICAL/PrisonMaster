using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class PrisonMover : MonoBehaviour
{
    [SerializeField] private GameObject _stickmanModel;
    [SerializeField] private GameObject _prisonerModel;
    
    private GameObject _nextPoint;
    private NavMeshAgent _agent;
    private Animator _animator;
    private Vector3 _targetPoint;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private NavMeshPath _agentPath;
    private bool _pathEnded;
    private Transform _lookTarget;
    
    public GameObject NextPoint =>_nextPoint;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
       if (_agentPath != _agent.path)
       {
           _agent.destination = _nextPoint.transform.position + _targetPoint;
           _agentPath = _agent.path;
       }

       transform.LookAt(_lookTarget);
        
       _animator.SetFloat(Speed, _agent.velocity.magnitude/_agent.speed);
    }

    public bool PathEnded()
    {
        _pathEnded = false;
        
        if(_agent.remainingDistance <= _agent.stoppingDistance)
            _pathEnded = true;
        
        return _pathEnded;
    }

    public void SetTarget(GameObject target, Vector3 offset,Transform lookTarget = null)
    {
        _nextPoint = target;
        _targetPoint = offset;
        
        if (lookTarget == null)
            _lookTarget = _nextPoint.transform;
        else
            _lookTarget = lookTarget;


    }

    public void ChangePriority(int value)
    {
        _agent.avoidancePriority = value;
    }
}