using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Agava.IdleGame;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private SmoothTextValue _smoothTextValue;
    [SerializeField] private SoftCurrencyHolder _playerBalance;

    private int _currentValue = -1;

    private void OnEnable()
    {
        _playerBalance.BalanceChanged += OnNewCountMoney;
    }

    private void OnDisable()
    {
        _playerBalance.BalanceChanged -= OnNewCountMoney;
    }

    private void OnNewCountMoney(int value)
    {
        if (_currentValue == -1)
            _currentValue = value;

        _smoothTextValue.StartSmooth(_text, _currentValue, value);
        _currentValue = value;
    }
}
