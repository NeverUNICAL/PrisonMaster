using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : UnlockableMapZone
{
    [SerializeField] private Transform _fabric;

    private bool _isFirst = true;
    private int _counter = 0;
    private int _pairCounter = 0;

    public override void Unlock(BuyZonePresenter buyZone)
    {
        int tempCounter = 0;
        int target = 2;

        if (_isFirst)
        {
            UnlockNextLevelZone();
            _isFirst = false;

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
        }

        tempCounter = 0;
        _pairCounter += 2;
        _counter++;

        if (_pairCounter == 12)
            _pairCounter++;

        TryUnlockPairZones(tempCounter, target);
    }

    public override void UnlockNextLevel(BuyZonePresenter buyZone)
    {
        for (int i = 0; i < NextZones.Length; i++)
            NextZones[i].Unlock();

        _fabric.gameObject.SetActive(true);
    }

    private void TryUnlockPairZones(int tempCounter, int target)
    {
        if (_pairCounter % 2 == 0)
        {
            for (int i = 0; i < BuyZones.Length; i++)
            {
                if (BuyZones[i].gameObject.activeInHierarchy == false && _counter == target)
                {
                    if (tempCounter < target)
                    {
                        AnimationScale(BuyZones[i].transform);
                        tempCounter++;
                    }
                    else
                    {
                        _counter = 0;
                    }
                }
            }
        }
        else
        {
            UnlockSingleZones();
        }
    }

    private void UnlockSingleZones()
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
