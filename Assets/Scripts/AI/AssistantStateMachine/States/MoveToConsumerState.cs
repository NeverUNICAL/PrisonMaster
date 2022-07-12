using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToConsumerState : AssistantState
{
    [SerializeField] private float _speed;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
     //   transform.position = Vector3.MoveTowards(transform.position, ConsumerTarget.transform.position, _speed * Time.deltaTime);
     //   transform.LookAt(ConsumerTarget.transform.position);
        _navMeshAgent.SetDestination(ConsumerTarget.transform.position);
    }
}
