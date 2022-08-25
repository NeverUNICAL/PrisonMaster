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
   [SerializeField] private float _timeBeforePressBrake = 2.2f;
   [SerializeField] private float _timeToCloseDoor = 3f;
   [SerializeField] private float _timeToRefreshPosition = 1f;
   [SerializeField] private float _breakPower = 0.05f;
   [SerializeField] private float _timeToRefreshWay = 5f;
   [SerializeField] private StartPool _startPool;
   [SerializeField] private ParticleSystem _fadingParticleSystem;
   [SerializeField] private ParticleSystem _leftSmoke;
   [SerializeField] private ParticleSystem _rightSmoke;

   private NavMeshAgent _agent;
   private float _timeToDestroyFadingParticle = 2f;
   private MeshRenderer _meshRenderer;
   private Vector3 _startPosition;
   private bool _brakePressed;
   private bool _doorOpened;
   private float _startSpeed;
   private bool _isFirstTriggerEnter;
   private ParticleSystem _instantiatedParticleSystem;

   private void Start()
   {
      _meshRenderer = GetComponent<MeshRenderer>();
      _agent = GetComponent<NavMeshAgent>();
      _startPosition = transform.position;
      _startSpeed = _agent.speed;
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
      _trigger.Enter -= OnEnter;
      _agent.SetDestination(_travelPoint.position);
      Invoke(nameof(PressBrake),_timeBeforePressBrake);
     
   }

   private void OpenDoor()
   {
      _startPool.ChangeSpawningState(true);
      _brakePressed = false;
      Invoke(nameof(CloseDoor),_timeToCloseDoor);
   }

   private void CloseDoor()
   {
      _startPool.ChangeSpawningState(false);
      _doorOpened = false;
      Invoke(nameof(RefreshPosition),_timeToRefreshPosition);
      Invoke(nameof(RefreshWay),_timeToRefreshWay);
   }

   private void RefreshPosition()
   {
      _leftSmoke.gameObject.SetActive(false);
      _rightSmoke.gameObject.SetActive(false);
      _instantiatedParticleSystem = Instantiate(_fadingParticleSystem, transform);
      _instantiatedParticleSystem.gameObject.transform.SetParent(null);
      Destroy(_instantiatedParticleSystem.gameObject,_timeToDestroyFadingParticle);
      transform.position = _startPosition;
      _agent.SetDestination(_startPosition);
   }

   private void RefreshWay()
   {
      _agent.speed = _startSpeed;
      _agent.SetDestination(_travelPoint.position);
      Invoke(nameof(PressBrake),_timeBeforePressBrake);
   }

   private void PressBrake()
   {
      _leftSmoke.gameObject.SetActive(true);
      _rightSmoke.gameObject.SetActive(true);
      _brakePressed = true;
   }
}
