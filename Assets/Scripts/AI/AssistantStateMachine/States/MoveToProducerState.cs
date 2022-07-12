using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToProducerState : AssistantState
{
    [SerializeField] private float _speed;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
       // transform.position = Vector3.MoveTowards(transform.position, ProducerTarget.transform.position, _speed * Time.deltaTime);
       // transform.LookAt(ProducerTarget.transform.position);
       _navMeshAgent.SetDestination(ProducerTarget.transform.position);
    }
}
