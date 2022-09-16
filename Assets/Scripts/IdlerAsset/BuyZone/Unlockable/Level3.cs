using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : UnlockableMapZone
{
    [SerializeField] private LevelBuyZone _fourthLevel;

    private bool _isFourthLevelOpened = false;
    private bool _isFirst = true;

    private void OnEnable()
    {
        base.OnEnable();
        _fourthLevel.Unlocked += OnUnlockFourthZone;
    }

    private void OnDisable()
    {
        base.OnDisable();
        _fourthLevel.Unlocked -= OnUnlockFourthZone;
    }

    public override void Unlock(BuyZonePresenter buyZone)
    {
        if (_isFirst)
        {
            UnlockNextLevelZone();
            _isFirst = false;
        }

        int tempCounter = 0;
        int target = 2;
        Counter++;

        for (int i = 0; i < BuyZones.Length; i++)
        {
            if (BuyZones[i].gameObject.activeInHierarchy == false && Counter == target)
            {
                if (tempCounter < target)
                {
                    AnimationScale(BuyZones[i].transform);
                    tempCounter++;
                }
                else
                {
                    Counter = 0;
                }
            }
        }
    }

    public override void UnlockNextLevel(BuyZonePresenter buyZone)
    {
        for (int i = 0; i < NextZones.Length; i++)
            NextZones[i].Unlock();
    }

    private void OnUnlockFourthZone(BuyZonePresenter buyZone)
    {
        _isFourthLevelOpened = true;
        Counter++;
        Unlock(null);
    }
}
