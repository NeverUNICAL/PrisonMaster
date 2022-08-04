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
    [SerializeField] private GameObject _background;
    
    [SerializeField] private Trigger _hrTrigger;
    [SerializeField] private Trigger _upgradesTrigger;

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
        _background.SetActive(true);
    }
    
    private void OnUpgradesEnter()
    {
        _upgradesPanel.SetActive(true);
        _background.SetActive(true);
    }
    
    private void OnHRExit()
    {
        _hrPanel.SetActive(false);
        _background.SetActive(false);
    }
    
    private void OnUpgradesExit()
    {
        _upgradesPanel.SetActive(false);
        _background.SetActive(false);
    }
}
