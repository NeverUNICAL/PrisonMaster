using System;
using System.Collections;
using System.Collections.Generic;
using Agava.IdleGame;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    [Header("Stack Settings")]
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private int _secondLevelCapacity;
    [SerializeField] private int _thirdLevelCapacity;
    [SerializeField] private Button _capacityButton;
    
    [Header("Speed Settings")]
    [SerializeField] private Button _speedButton;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _secondLevelSpeed;
    [SerializeField] private float _thirdLevelSpeed;
    
    private void OnEnable()
    {
        _capacityButton.onClick.AddListener(OnCapacityButtonClick);
        _speedButton.onClick.AddListener(OnSpeedButtonClick);
    }

    private void OnDisable()
    {
        _capacityButton.onClick.RemoveListener(OnCapacityButtonClick);
        _speedButton.onClick.RemoveListener(OnSpeedButtonClick);
    }

    private void OnCapacityButtonClick()
    {
        if (_stackPresenter.Capacity != _secondLevelCapacity && _stackPresenter.Capacity != _thirdLevelCapacity)
        {
            _stackPresenter.ChangeCapacity(_secondLevelCapacity);
        }
        else if (_stackPresenter.Capacity == _secondLevelCapacity)
        {
            _stackPresenter.ChangeCapacity(_thirdLevelCapacity);
        }
    }
    
    private void OnSpeedButtonClick()
    {
        if (_navMeshAgent.speed != _secondLevelSpeed && _navMeshAgent.speed != _thirdLevelSpeed)
        {
            _navMeshAgent.speed = _secondLevelSpeed;
        }
        else if (_navMeshAgent.speed == _secondLevelSpeed)
        {
            _navMeshAgent.speed = _thirdLevelSpeed;
        }
    }
}
