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
    [SerializeField] private Transform _travelPoint;
    [SerializeField] private Transform _outPoint;
    [SerializeField] private float _timeToCloseDoor = 3f;
    [SerializeField] private float _breakPower = 0.001f;
    [SerializeField] private float _timeToRefreshWay = 5f;
    [SerializeField] private StartPool _startPool;
    [SerializeField] private ParticleSystem _leftSmoke;
    [SerializeField] private ParticleSystem _rightSmoke;
    [SerializeField] private GlobalTutorial _globalTutorial;

    private float _defaultBreakPower;
    private NavMeshAgent _agent;
    private MeshRenderer _meshRenderer;
    private Vector3 _startPosition;
    private bool _brakePressed;
    private bool _doorOpened;
    private float _startSpeed;
    private bool _isFirst = true;
    private bool _isTutorialCompleted = false;

    private void OnEnable()
    {
        _globalTutorial.GloalTutorialCompleted += OnTutorialCompleted;
    }

    private void OnDisable()
    {
        _globalTutorial.GloalTutorialCompleted -= OnTutorialCompleted;
    }

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _agent = GetComponent<NavMeshAgent>();
        _startPosition = transform.position;
        _startSpeed = _agent.speed;
        _defaultBreakPower = _breakPower;

        _agent.SetDestination(_travelPoint.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TriggerForBus trigger))
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

    private void OnTutorialCompleted()
    {
        _timeToCloseDoor = 5;
        _isTutorialCompleted = true;
        RefreshWay();
    }

    private void OpenDoor()
    {
        if (_isFirst || _isTutorialCompleted)
        {
            if (_startPool != null)
                _startPool.SetWorking(true);

            Invoke(nameof(CloseDoor), _timeToCloseDoor);
        }
    }

    private void CloseDoor()
    {
        _isFirst = false;
        if (_startPool != null)
            _startPool.SetWorking(false);

        _doorOpened = false;
        _brakePressed = false;
        ChangeSmokeActive(false);
        _agent.speed = _startSpeed;
        _agent.SetDestination(_outPoint.position);
        Invoke(nameof(RefreshWay), _timeToRefreshWay);
    }

    private void RefreshWay()
    {
        transform.position = _startPosition;
        _agent.speed = _startSpeed;
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
