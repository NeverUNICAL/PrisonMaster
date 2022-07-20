using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class PrisonMover : MonoBehaviour
{
    private GameObject _nextPoint;
    private NavMeshAgent _agent;
    private Animator _animator;
    public GameObject NextPoint =>_nextPoint;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        _agent.destination = _nextPoint.transform.position;
        _animator.SetFloat("Speed", _agent.velocity.magnitude/_agent.speed);
    }

    public void SetTarget(GameObject target)
    {
        _nextPoint = target;
    }
}