using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : UnlockableMapZone
{
    [SerializeField] private GlobalTutorial _globalTutorial;

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
        {
            NextZones[i].Unlock();
            
        }
    }

    private void OnGlobalTutorialComplete()
    {
        Counter++;
        Unlock(null);

        _globalTutorial.GloalTutorialCompleted -= OnGlobalTutorialComplete;
    }
}
