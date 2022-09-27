using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : UnlockableMapZone
{
    [SerializeField] private GlobalTutorial _globalTutorial;

    private bool _isFirst = true;
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

    private void OnGlobalTutorialComplete()
    {
        _isFourthLevelOpened = true;

        _globalTutorial.GloalTutorialCompleted -= OnGlobalTutorialComplete;
    }

    public override void Unlock(BuyZonePresenter buyZone)
    {
        if (_isFourthLevelOpened && buyZone != Room)
            UnlockBuyZone();
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

    public override void UnlockNextLevel(BuyZonePresenter buyZone)
    {
    }
}
