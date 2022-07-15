using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class PrisonerIdleState : PrisonerState
{
    [SerializeField] private bool _isStarted = false;
    [SerializeField] private float _duration;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_isStarted)
            transform.DORotateQuaternion(Prisoner.PositionHandler.transform.rotation, _duration);

        if (Prisoner.NavMeshAgent.enabled == true)
            Prisoner.ChangeWorkNavMesh(false);

        _animator.Play("Idle");
    }
}