using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class PrisonMover : MonoBehaviour
{
    private GameObject _nextPoint;
    private NavMeshAgent _agent;
    private Animator _animator;
    private Vector3 _targetPoint;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private NavMeshPath _agentPath;
    
    public GameObject NextPoint =>_nextPoint;
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
       if (_agentPath != _agent.path)
       {
           _agent.destination = _targetPoint;
           _agentPath = _agent.path;
       }
        
       _animator.SetFloat(Speed, _agent.velocity.magnitude/_agent.speed);
    }

    public void SetTarget(GameObject target, Vector3 offset)
    {
        _nextPoint = target;
        _targetPoint = _nextPoint.transform.position+offset;
    }
}