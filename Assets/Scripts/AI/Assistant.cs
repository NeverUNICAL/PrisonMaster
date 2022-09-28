using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Assistant : MonoBehaviour
{
    [Header("OptionsForUpgrade")]
    [SerializeField] private float _speed;

    [SerializeField] private AssistantBuyZone _buyZone;
    [SerializeField] private PlayerMovement _player;

    protected NavMeshAgent NavMeshAgent;
    private PlayerStackPresenter _stackPresenter;
    private AssistantStateMachine _stateMachine;
    private Transform _targetTransform;

    public PlayerMovement Player => _player;
    public AssistantBuyZone BuyZone => _buyZone;
    public PlayerStackPresenter StackPresenter => _stackPresenter;
    public float Speed => _speed;
    public int Capacity => _stackPresenter.Capacity;
    public Transform TargetTransform => _targetTransform;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        _stackPresenter = GetComponent<PlayerStackPresenter>();
        _stateMachine = GetComponent<AssistantStateMachine>();
    }

    public void ChangeSpeed(float targetSpeed)
    {
        _speed = targetSpeed;
        NavMeshAgent.speed = targetSpeed;
    }

    public void ChangeCapacity(int target)
    {
        _stackPresenter.ChangeCapacity(target);
    }

    public void ChangeTargetTransform(Transform target)
    {
        _targetTransform = target;
    }

    public void Move(Vector3 target)
    {
        NavMeshAgent.SetDestination(target);
    }

    protected void StartStateMachine()
    {
        _stateMachine.enabled = true;
    }
}
