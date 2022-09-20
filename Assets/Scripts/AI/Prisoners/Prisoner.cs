using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Prisoner : MonoBehaviour
{
    protected NavMeshPath AgentPath;
    private NavMeshAgent _navMeshAgent;
    protected Animator Animator;

    protected const string IsAngryAnimation = "IsAngry";
    protected static readonly int Speed = Animator.StringToHash("Speed");

    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
    }

    protected void FixedUpdate()
    {
        Animator.SetFloat(Speed, NavMeshAgent.velocity.magnitude / NavMeshAgent.speed);
    }
}
