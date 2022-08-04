using System;
using System.Collections;
using System.Collections.Generic;
using Agava.IdleGame;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class AssistantsShop : MonoBehaviour
{
    [Header("Count Settings")] 
    [SerializeField] private Button _countButton;
    [SerializeField] private RectTransform _fillCountImage;
    [SerializeField] private TextMeshProUGUI _countPriceText;
    [SerializeField] private List<Upgrade> _countUpgrades;
    
    [Header("Capacity Settings")]
    [SerializeField] private Button _capacityButton;
    [SerializeField] private RectTransform _fillCapacityImage;
    [SerializeField] private TextMeshProUGUI _capacityPriceText;
    [SerializeField] private List<Upgrade> _capacityUpgrades;

    [Header("Speed Settings")]
    [SerializeField] private Button _speedButton;
    [SerializeField] private RectTransform _fillSpeedImage;
    [SerializeField] private TextMeshProUGUI _speedPriceText;
    [SerializeField] private List<Upgrade> _speedUpgrades;

    [Header("Settings")]
    [SerializeField] private Image _background;
    [SerializeField] private Button _exitButton;
    [SerializeField] private List<Assistant> _assistants;
    [SerializeField] private AssistantsSavePresenter _saves;
    [SerializeField] private PlayerSavePresenter _playerSaves;
    [SerializeField] private float _imageFillWidthStep;
    [SerializeField] private int _upgradesMaxLevel = 4;

    public event UnityAction<int,float,int> SpeedUpgraded;
    public event UnityAction<int,int,int> CapacityUpgraded;
    public event UnityAction<int, int> CountUpgraded;

    private void OnEnable()
    {
        _capacityButton.onClick.AddListener(OnCapacityButtonClick);
        _countButton.onClick.AddListener(OnCountButtonClick);
        _speedButton.onClick.AddListener(OnSpeedButtonClick);
        _saves.Loaded += OnLoaded;
    }

    private void OnDisable()
    { 
        _capacityButton.onClick.RemoveListener(OnCapacityButtonClick);
        _countButton.onClick.RemoveListener(OnCountButtonClick); 
        _speedButton.onClick.RemoveListener(OnSpeedButtonClick);
        _saves.Loaded -= OnLoaded;
    }

    private void OnCapacityButtonClick()
    {
        if (_saves.Count > 0)
        {
            foreach (Upgrade upgrade in _capacityUpgrades)
            {
                if (upgrade.Level == _saves.CapacityLevel + 1 && upgrade.Price <= _saves.Money)
                {
                    CapacityUpgraded?.Invoke(upgrade.Level, (int)upgrade.Value, upgrade.Price);
                    ResetAssistants();
                    _fillCapacityImage.sizeDelta = new Vector2(_fillCapacityImage.rect.width + _imageFillWidthStep, _fillCapacityImage.rect.height);
                    ResetPriceView(_capacityUpgrades,_saves.CapacityLevel,_capacityPriceText);
                    return;
                }

            }
        }
    }
    
    private void OnSpeedButtonClick()
    {
        if (_saves.Count > 0)
        {
            foreach (Upgrade upgrade in _speedUpgrades)
            {
                if (upgrade.Level == _saves.SpeedLevel + 1 && upgrade.Price <= _saves.Money)
                {
                    SpeedUpgraded?.Invoke(upgrade.Level, upgrade.Value, upgrade.Price);
                    ResetAssistants();
                    _fillSpeedImage.sizeDelta = new Vector2(_fillSpeedImage.rect.width + _imageFillWidthStep, _fillSpeedImage.rect.height);
                    ResetPriceView(_speedUpgrades,_saves.SpeedLevel,_speedPriceText);
                    return;
                }
            }
        }
    }

    private void OnCountButtonClick()
    {
        if (_saves.Count < _upgradesMaxLevel)
        {
            foreach (Upgrade upgrade in _countUpgrades)
            {
                if (upgrade.Level == _saves.Count + 1 && upgrade.Price <= _saves.Money)
                {
                    ChangeStateTutorial(true, false);
                    CountUpgraded?.Invoke(upgrade.Level,upgrade.Price);
                    ResetAssistants();
                    _fillCountImage.sizeDelta = new Vector2(_fillCountImage.rect.width + _imageFillWidthStep, _fillCountImage.rect.height);
                    ResetPriceView(_countUpgrades,_saves.Count,_countPriceText);
                    return;
                }
            }
        }
    }

    private void ResetPriceView(List<Upgrade> upgrades, int playerLevel,TextMeshProUGUI text)
    {
        if (playerLevel == _upgradesMaxLevel)
        {
            text.text = "MAX";
        }
       
        else
        {
            foreach (Upgrade upgrade in upgrades)
            {
                if (upgrade.Level == playerLevel + 1)
                {
                    if (upgrade.Price == 0)
                    {
                        text.text = "FREE";
                        return;
                    }
                    
                    if(upgrade.Price >= 10000)
                    {
                        var price = upgrade.Price / 1000;
                        text.text = "$" + price + "K";
                        return;
                    }
                    
                    text.text = "$" + upgrade.Price;
                    return;
                }
            }
        }
    }

    private void ResetAssistants()
    {
        if (_saves.Count > 0)
        {
            for (int i = 0; i < _saves.Count; i++)
            {
                _assistants[i].gameObject.SetActive(true);
                
                if(_saves.Capacity > 0)
                 _assistants[i].ChangeCapacity(_saves.Capacity);
                
                if(_saves.Speed > 0)
                 _assistants[i].ChangeSpeed(_saves.Speed);
            }
        }
    }

    private void OnLoaded()
    {
        ResetAssistants();
        ResetPriceView(_capacityUpgrades,_saves.CapacityLevel,_capacityPriceText);
        ResetPriceView(_speedUpgrades,_saves.SpeedLevel,_speedPriceText);
        ResetPriceView(_countUpgrades,_saves.Count,_countPriceText);
        _fillCapacityImage.sizeDelta = new Vector2(_fillCapacityImage.rect.width + (_imageFillWidthStep*_saves.CapacityLevel), _fillCapacityImage.rect.height);
        _fillSpeedImage.sizeDelta = new Vector2(_fillSpeedImage.rect.width + (_imageFillWidthStep*_saves.SpeedLevel), _fillSpeedImage.rect.height);
        _fillCountImage.sizeDelta = new Vector2(_fillCountImage.rect.width + (_imageFillWidthStep*_saves.Count), _fillCountImage.rect.height);
    }

    public void ChangeStateTutorial(bool interactableValue, bool value)
    {
        if (_saves.Count == 0)
        {
        _exitButton.interactable = interactableValue;
        _capacityButton.interactable = interactableValue;
        _speedButton.interactable = interactableValue;
        _background.gameObject.SetActive(value);
        }
    }
}
