using System;
using System.Collections;
using System.Collections.Generic;
using Agava.IdleGame;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _hrPanel;
    [SerializeField] private GameObject _upgradesPanel;
    
    [SerializeField] private Trigger _hrTrigger;
    [SerializeField] private Trigger _upgradesTrigger;

    private AssistantsShop _assistantsShop;
    private UpgradesShop _upgradesShop;

    private void Awake()
    {
        _assistantsShop = GetComponent<AssistantsShop>();
        _upgradesShop = _upgradesPanel.GetComponent<UpgradesShop>();
    }

    private void OnEnable()
    {
        _hrTrigger.Enter += OnHREnter;
        _hrTrigger.Exit += OnHRExit;
        _upgradesTrigger.Enter += OnUpgradesEnter;
        _upgradesTrigger.Exit += OnUpgradesExit;
    }

    private void OnDisable()
    {
        _hrTrigger.Enter -= OnHREnter;
        _hrTrigger.Exit -= OnHRExit;
        _upgradesTrigger.Enter -= OnUpgradesEnter;
        _upgradesTrigger.Exit -= OnUpgradesExit;
    }

    private void OnHREnter()
    {
        _hrPanel.SetActive(true);
        _assistantsShop.TryChangeState(false, true);
    }
    
    private void OnUpgradesEnter()
    {
        _upgradesPanel.SetActive(true);
        _upgradesShop.TryChangeState(false, true);
    }
    
    private void OnHRExit()
    {
        _hrPanel.SetActive(false);
    }
    
    private void OnUpgradesExit()
    {
        _upgradesPanel.SetActive(false);
    }
}
