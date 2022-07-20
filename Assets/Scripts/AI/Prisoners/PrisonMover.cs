using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class PrisonMover : MonoBehaviour
{
    private GameObject _nextPoint;
    private NavMeshAgent _agent;
    private Animator _animator;
    public GameObject NextPoint =>_nextPoint;
    private Vector3 _targetPoint;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        _agent.destination = _targetPoint;
        _animator.SetFloat("Speed", _agent.velocity.magnitude/_agent.speed);
    }

    public void SetTarget(GameObject target, Vector3 offset)
    {
        _nextPoint = target;
        _targetPoint = _nextPoint.transform.position+offset;
    }
}