using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Agava.IdleGame.Model;
using Agava.IdleGame;
using UnityEngine;
using UnityEngine.AI;

public class AssistantAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Assistant _assistant;
    private NavMeshAgent _navMeshAgent;
    private const string Speed = "Speed";
    private const string IsStandingGreeting = "IsStandingGreeting";
    private const string IsMoving = "IsMoving";
    private bool _isLookAt;

    private void Awake()
    {
        _assistant = GetComponent<Assistant>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _assistant.BuyZone.Unlocked += OnUnlocked;
    }

    private void OnDisable()
    {
        _assistant.BuyZone.Unlocked -= OnUnlocked;
    }

    private void Start()
    {
        _animator.SetTrigger(IsStandingGreeting);
        _isLookAt = true;
    }

    private void Update()
    {
        _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude);

        if (_isLookAt)
            transform.LookAt(_assistant.Player.transform);
    }

    private void OnUnlocked(BuyZonePresenter buyZone)
    {
        _isLookAt = false;
        _animator.SetTrigger(IsMoving);
    }
}
