using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wheel : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _agentSpeedForRotating;

    private Vector3 _rotation;

    private void Start()
    {
        _rotation = new Vector3(-_rotateSpeed, 0, 0);
    }

    private void Update()
    {
        if(_agent.speed > _agentSpeedForRotating)
         gameObject.transform.Rotate(_rotation*Time.deltaTime);
    }
}
