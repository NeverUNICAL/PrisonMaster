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

public class UpgradesShop : MonoBehaviour
{
    [Header("Capacity Settings")]
    [SerializeField] private Button _capacityButton;
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private RectTransform _fillCapacityImage;
    [SerializeField] private TextMeshProUGUI _capacityPriceText;
    [SerializeField] private List<Upgrade> _capacityUpgrades;
    [SerializeField] private Image _imageForBlockCapacity;
    
    [Header("Speed Settings")]
    [SerializeField] private Button _speedButton;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private RectTransform _fillSpeedImage;
    [SerializeField] private TextMeshProUGUI _speedPriceText;
    [SerializeField] private List<Upgrade> _speedUpgrades;
    [SerializeField] private Image _imageForBlockSpeed;
    [SerializeField] private Vector2 _defaultSize;

    [Header("Settings")]
    [SerializeField] private RectTransform _arrowUI;
    [SerializeField] private Animator _iconAnimatorTutorial;
    [SerializeField] private Image _background;

    [SerializeField] private Button _exitButton;
    [SerializeField] private PlayerSavePresenter _playerSavePresenter;
    [SerializeField] private float _imageFillWidthStep;
    [SerializeField] private int _upgradesMaxLevel = 4;


    public event UnityAction<int, float, int> SpeedUpgraded;
    public event UnityAction<int, int, int> CapacityUpgraded;

    private void OnEnable()
    {
        _capacityButton.onClick.AddListener(OnCapacityButtonClick);
        _speedButton.onClick.AddListener(OnSpeedButtonClick);
        _playerSavePresenter.Loaded += OnLoaded;
        ResetPriceView(_capacityUpgrades, _playerSavePresenter.CapacityLevel, _capacityPriceText,_imageForBlockCapacity);
        ResetPriceView(_speedUpgrades, _playerSavePresenter.SpeedLevel, _speedPriceText,_imageForBlockSpeed);
    }

    private void OnDisable()
    {
        _capacityButton.onClick.RemoveListener(OnCapacityButtonClick);
        _speedButton.onClick.RemoveListener(OnSpeedButtonClick);
        _playerSavePresenter.Loaded -= OnLoaded;
    }

    private void Start()
    {
        OnLoaded();

        if (_playerSavePresenter.SpeedLevel == 0)
            ChangeState(false, true);
    }

    private void OnCapacityButtonClick()
    {
        foreach (Upgrade upgrade in _capacityUpgrades)
        {
            if (upgrade.Level - 1 == _playerSavePresenter.CapacityLevel
                && _stackPresenter.Capacity < upgrade.Value
                && _playerSavePresenter.Money >= upgrade.Price)
            {
                CapacityUpgraded?.Invoke(upgrade.Level, (int)upgrade.Value, upgrade.Price);
                _fillCapacityImage.sizeDelta = new Vector2(_fillCapacityImage.rect.width + _imageFillWidthStep, _fillCapacityImage.rect.height);
                ResetPriceView(_capacityUpgrades, _playerSavePresenter.CapacityLevel, _capacityPriceText,_imageForBlockCapacity);
                ResetPriceView(_speedUpgrades, _playerSavePresenter.SpeedLevel, _speedPriceText,_imageForBlockSpeed);
                return;
            }
        }
    }

    private void OnSpeedButtonClick()
    {
        foreach (Upgrade upgrade in _speedUpgrades)
        {
            if (upgrade.Level - 1 == _playerSavePresenter.SpeedLevel
                && _navMeshAgent.speed < upgrade.Value
                && _playerSavePresenter.Money >= upgrade.Price)
            {
                SpeedUpgraded?.Invoke(upgrade.Level, upgrade.Value, upgrade.Price);
                _fillSpeedImage.sizeDelta = new Vector2(_fillSpeedImage.rect.width + _imageFillWidthStep, _fillSpeedImage.rect.height);
                ResetPriceView(_capacityUpgrades, _playerSavePresenter.CapacityLevel, _capacityPriceText,_imageForBlockCapacity);
                ResetPriceView(_speedUpgrades, _playerSavePresenter.SpeedLevel, _speedPriceText,_imageForBlockSpeed);
                ChangeState(true, false);
                return;
            }
        }
    }

    private void ResetPriceView(List<Upgrade> upgrades, int playerLevel, TextMeshProUGUI text,Image image)
    {
        if (playerLevel == _upgradesMaxLevel)
        {
            text.text = "MAX";
        }
        else
        {
            foreach (Upgrade upgrade in upgrades)
            {
                if (upgrade.Price > _playerSavePresenter.Money)
                {
                    image.gameObject.SetActive(true);
                }
                else
                {
                    image.gameObject.SetActive(false);
                }
                
                if (upgrade.Level == playerLevel + 1)
                {
                    if (upgrade.Price == 0)
                    {
                        text.text = "FREE";
                        return;
                    }

                    if (upgrade.Price >= 10000)
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

    private void OnLoaded()
    {
        ResetPriceView(_capacityUpgrades, _playerSavePresenter.CapacityLevel, _capacityPriceText,_imageForBlockCapacity);
        ResetPriceView(_speedUpgrades, _playerSavePresenter.SpeedLevel, _speedPriceText,_imageForBlockSpeed);
        _fillCapacityImage.sizeDelta = new Vector2(_fillCapacityImage.rect.width + (_imageFillWidthStep * _playerSavePresenter.CapacityLevel), _fillCapacityImage.rect.height);
        _fillSpeedImage.sizeDelta = new Vector2(_fillSpeedImage.rect.width + (_imageFillWidthStep * _playerSavePresenter.SpeedLevel), _fillSpeedImage.rect.height);
    }

    public void TryChangeState(bool interactableValue, bool value)
    {
        if (_playerSavePresenter.SpeedLevel == 0)
            ChangeState(interactableValue, value);
    }

    private void ChangeState(bool interactableValue, bool value)
    {
        _exitButton.interactable = interactableValue;
        _capacityButton.interactable = interactableValue;
        _iconAnimatorTutorial.enabled = value;
        _background.gameObject.SetActive(value);
        _arrowUI.gameObject.SetActive(value);

        if (interactableValue)
            _speedButton.image.rectTransform.sizeDelta = _defaultSize;
    }
}
