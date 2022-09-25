using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : UnlockableMapZone
{
    //[SerializeField] private Transform[] _deposits;
    //[SerializeField] private ObjectCreator _producer;
    //[SerializeField] private Transform _prisonersManager;
    [SerializeField] private MoneySpawner _moneySpawner;

    private bool _isFirst = true;
    private bool _isFourthLevelOpened = false;

    private void OnEnable()
    {
        base.OnEnable();
        _moneySpawner.MoneySpawned += OnUnlockFourthZone;
    }

    private void OnDisable()
    {
        base.OnDisable();
    }

    private void OnUnlockFourthZone()
    {
        _isFourthLevelOpened = true;
        UnlockBuyZone();

        _moneySpawner.MoneySpawned -= OnUnlockFourthZone;
    }

    public override void Unlock(BuyZonePresenter buyZone)
    {
        //if (_isFirst)
        //    UnlockNextLevelZone();

        if (_isFourthLevelOpened)
            UnlockBuyZone();
    }

    public override void UnlockNextLevel(BuyZonePresenter buyZone)
    {
        //for (int i = 0; i < NextZones.Length; i++)
        //    NextZones[i].Unlock();

        //for (int i = 0; i < _deposits.Length; i++)
        //    _deposits[i].gameObject.SetActive(true);

        //_prisonersManager.gameObject.SetActive(true);
        //_producer.gameObject.SetActive(true);
    }

    private void UnlockBuyZone()
    {
        for (int i = 0; i < BuyZones.Length; i++)
        {
            if (BuyZones[i].gameObject.activeInHierarchy == false)
            {
                if (_isFirst)
                {
                    AnimationScale(BuyZones[i].transform);
                    _isFirst = false;
                }
                else
                {
                    AnimationScale(BuyZones[i].transform);
                    return;
                }
            }
        }
    }
}
