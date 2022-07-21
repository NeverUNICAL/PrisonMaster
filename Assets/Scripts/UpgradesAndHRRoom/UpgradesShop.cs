using System;
using System.Collections;
using System.Collections.Generic;
using Agava.IdleGame;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradesShop : MonoBehaviour
{
    [Header("Capacity Settings")]
    [SerializeField] private Button _capacityButton;
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private RectTransform _fillCapacityImage;
    [SerializeField] private List<Upgrade> _capacityUpgrades;
    
    [Header("Speed Settings")]
    [SerializeField] private Button _speedButton;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private RectTransform _fillSpeedImage;
    [SerializeField] private List<Upgrade> _speedUpgrades;
    
    [Header("Settings")] 
    [SerializeField] private PlayerSavePresenter _playerSavePresenter;

    [SerializeField] private float _imageFillWidthStep;
    
    public event UnityAction<int,float,int> SpeedUpgraded;
    public event UnityAction<int,int,int> CapacityUpgraded;
    

    private void OnEnable()
    {
        _capacityButton.onClick.AddListener(OnCapacityButtonClick);
        _speedButton.onClick.AddListener(OnSpeedButtonClick);
        _playerSavePresenter.Loaded += OnLoaded;
    }

    private void OnDisable()
    {
        _capacityButton.onClick.RemoveListener(OnCapacityButtonClick);
        _speedButton.onClick.RemoveListener(OnSpeedButtonClick);
        _playerSavePresenter.Loaded -= OnLoaded;
    }
    
    private void OnCapacityButtonClick()
    {
        foreach (Upgrade upgrade in _capacityUpgrades)
        {
            if (upgrade.Level - 1 == _playerSavePresenter.CapacityLevel
                && _stackPresenter.Capacity < upgrade.Value
                && _playerSavePresenter.Money >= upgrade.Price)
            {
                CapacityUpgraded?.Invoke(upgrade.Level,(int)upgrade.Value,upgrade.Price);
                _fillCapacityImage.sizeDelta = new Vector2(_fillCapacityImage.rect.width + _imageFillWidthStep, _fillCapacityImage.rect.height);
                return;
            }
        }
    }
    
    private void OnSpeedButtonClick()
    {
        foreach (Upgrade upgrade in _capacityUpgrades)
        {
            if (upgrade.Level - 1 == _playerSavePresenter.SpeedLevel
                && _navMeshAgent.speed < upgrade.Value
                && _playerSavePresenter.Money >= upgrade.Price)
            {
                SpeedUpgraded?.Invoke(upgrade.Level,upgrade.Value,upgrade.Price);
                _fillSpeedImage.sizeDelta = new Vector2(_fillSpeedImage.rect.width + _imageFillWidthStep, _fillSpeedImage.rect.height);
                return;
            }
        }
    }

    private void OnLoaded()
    {
        _fillCapacityImage.sizeDelta = new Vector2(_fillCapacityImage.rect.width + (_imageFillWidthStep*_playerSavePresenter.CapacityLevel), _fillCapacityImage.rect.height);
        _fillSpeedImage.sizeDelta = new Vector2(_fillSpeedImage.rect.width + (_imageFillWidthStep*_playerSavePresenter.SpeedLevel), _fillSpeedImage.rect.height);
    }
}
