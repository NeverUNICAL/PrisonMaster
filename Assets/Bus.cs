using System;
using System.Collections;
using System.Collections.Generic;
using Agava.IdleGame;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bus : MonoBehaviour
{
    [SerializeField] private Trigger _trigger;
    [SerializeField] private Transform _travelPoint;
    [SerializeField] private Transform _outPoint;
    [SerializeField] private float _timeToCloseDoor = 3f;
    [SerializeField] private float _breakPower = 0.05f;
    [SerializeField] private float _timeToRefreshWay = 5f;
    [SerializeField] private StartPool _startPool;
    [SerializeField] private ParticleSystem _leftSmoke;
    [SerializeField] private ParticleSystem _rightSmoke;

    private NavMeshAgent _agent;
    private MeshRenderer _meshRenderer;
    private Vector3 _startPosition;
    private bool _brakePressed;
    private bool _doorOpened;
    private float _startSpeed;
    //private bool _isFirst = true;

    private void OnEnable()
    {
        _trigger.Enter += OnEnter;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnEnter;
    }

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _agent = GetComponent<NavMeshAgent>();
        _startPosition = transform.position;
        _startSpeed = _agent.speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Trigger trigger))
            OnBrake();
    }

    private void Update()
    {
        if (_brakePressed && _agent.remainingDistance <= _agent.stoppingDistance && _doorOpened == false)
        {
            _doorOpened = true;
            OpenDoor();
        }

        if (_brakePressed && _agent.speed > 1)
            _agent.speed -= _breakPower * Time.deltaTime;
    }


    private void OnEnter()
    {
        _trigger.Enter -= OnEnter;
        _agent.SetDestination(_travelPoint.position);
    }

    private void OpenDoor()
    {
        //if (_isFirst)
        //{
        _startPool.ChangeSpawningState(true);
        _brakePressed = false;
        Invoke(nameof(CloseDoor), _timeToCloseDoor);
        //}
    }

    private void CloseDoor()
    {
        //_isFirst = false;
        _startPool.ChangeSpawningState(false);
        _doorOpened = false;
        ChangeSmokeActive(false);
        _agent.speed = _startSpeed;
        _agent.SetDestination(_outPoint.position);
        Invoke(nameof(RefreshWay), _timeToRefreshWay);
    }

    private void RefreshWay()
    {
        transform.position = _startPosition;
        _agent.SetDestination(_travelPoint.position);
    }

    private void OnBrake()
    {
        ChangeSmokeActive(true);
        _brakePressed = true;
    }

    private void ChangeSmokeActive(bool value)
    {
        _leftSmoke.gameObject.SetActive(value);
        _rightSmoke.gameObject.SetActive(value);
    }
}
