using System;
using System.Collections;
using System.Collections.Generic;
using Agava.IdleGame;
using UnityEngine;
using UnityEngine.AI;

public class AssistantForCreo : MonoBehaviour
{
    [SerializeField] private Trigger _trigger;
    [SerializeField] private Transform _target;

    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _trigger.Enter += OnEnter;
    }
    
    private void OnDisable()
    {
        _trigger.Enter -= OnEnter;
    }

    private void OnEnter()
    {
        _navMeshAgent.SetDestination(_target.position);
    }
}
