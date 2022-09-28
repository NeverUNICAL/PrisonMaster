using Agava.IdleGame;
using ForCreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Level1 : UnlockableMapZone
{
    [SerializeField] private TrashZone _trashZone;
    [SerializeField] private GlobalTutorial _globalTutorial;

    private bool _isFourthLevelOpened = false;

    private void OnEnable()
    {
        base.OnEnable();
        _globalTutorial.GloalTutorialCompleted += OnGlobalTutorialComplete;
    }

    private void OnDisable()
    {
        base.OnDisable();
    }

    public override void Unlock(BuyZonePresenter buyZone)
    {
        if (_isFourthLevelOpened)
            UnlockBuyZone();
    }

    public override void UnlockNextLevel(BuyZonePresenter buyZone)
    {
        for (int i = 0; i < NextZones.Length; i++)
            NextZones[i].Unlock();

        AnimationScale(_trashZone.transform);
    }

    private void OnGlobalTutorialComplete()
    {
        _isFourthLevelOpened = true;
        UnlockBuyZone();

        _globalTutorial.GloalTutorialCompleted -= OnGlobalTutorialComplete;
    }

    private void UnlockBuyZone()
    {
        for (int i = 0; i < BuyZones.Length; i++)
        {
            if (BuyZones[i].gameObject.activeInHierarchy == false)
            {
                AnimationScale(BuyZones[i].transform);
                return;
            }
        }
    }
}