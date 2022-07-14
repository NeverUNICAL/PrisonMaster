using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Agava.IdleGame.Model;
using UnityEngine;
using UnityEngine.AI;

public class AssistantAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private NavMeshAgent _navMeshAgent;
    private const string Speed = "Speed";

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude);
    }
}
