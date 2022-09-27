using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level2 : UnlockableMapZone
{
    [SerializeField] private TrashZone _trashZone;
    [SerializeField] private GlobalTutorial _globalTutorial;

    private bool _isFourthLevelOpened = false;
    private bool _isFirst = true;
    private int _counter = 0;

    public event UnityAction RoomZoneOpened;

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

        int tempCounter = 0;
        int target = 4;

        for (int i = 0; i < BuyZones.Length; i++)
        {
            if (BuyZones[i].gameObject.activeInHierarchy == false)
            {
                if (tempCounter < target)
                {
                    AnimationScale(BuyZones[i].transform);
                    tempCounter++;
                }
            }
        }

        _globalTutorial.GloalTutorialCompleted -= OnGlobalTutorialComplete;
    }

    private void UnlockBuyZone()
    {
        _counter++;
        if (_counter > 3)
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
}
