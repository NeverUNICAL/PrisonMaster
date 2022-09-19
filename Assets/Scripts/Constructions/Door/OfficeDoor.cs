using System;
using System.Collections;
using System.Collections.Generic;
using Agava.IdleGame;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class OfficeDoor : MonoBehaviour
{
    [SerializeField] private float _inYMaxRotation = -270;
    [SerializeField] private float _outYMaxRotation = -90;
    [SerializeField] private Trigger _triggerIn;
    [SerializeField] private Trigger _triggerOut;
    
    private Vector3 _defaultRotation;
    private bool _isOpen = false;
    private bool _inTriggerAbandoned = true;
    private bool _outTriggerAbandoned = true;
    
    private void Start()
    {
        _defaultRotation = transform.rotation.eulerAngles;
    }

    private void OnEnable()
    {
        _triggerIn.Enter += OnEnterIn;
        _triggerOut.Enter += OnEnterOut;
        _triggerIn.Exit += OnExitIn;
        _triggerOut.Exit += OnExitOut;
    }

    private void OnDisable()
    {
        _triggerIn.Enter -= OnEnterIn;
        _triggerOut.Enter -= OnEnterOut;
        _triggerIn.Exit -= OnExitIn;
        _triggerOut.Exit -= OnExitOut;
    }

    private void OnEnterIn()
    {
        _inTriggerAbandoned = false;
        Open(_inYMaxRotation);
    }

    private void OnEnterOut()
    {
        _outTriggerAbandoned = false;
        Open(_outYMaxRotation);
    }

    private void OnExitOut()
    {
        _outTriggerAbandoned = true;

        if (_inTriggerAbandoned)
            Close();
    }
    
    private void OnExitIn()
    {
        _inTriggerAbandoned = true;
        
        if(_outTriggerAbandoned)
            Close();
    }

    private void Close()
    {
        if (_isOpen)
        {
            transform.DORotate(_defaultRotation, 0.2f);
            _isOpen = false;
        }
    }

    private void Open(float rotation)
    {
        if (_isOpen == false)
        {
            transform.DOLocalRotate(new Vector3(transform.rotation.x, rotation, transform.rotation.z), 0.2f);
            _isOpen = true;
        }
    }
}
