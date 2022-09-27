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
        Unlock(null);

        _globalTutorial.GloalTutorialCompleted -= OnGlobalTutorialComplete;
    }

    public override void Unlock(BuyZonePresenter buyZone)
    {
        if (_isFourthLevelOpened && buyZone != Room)
            UnlockBuyZone();
    }

    private void UnlockBuyZone()
    {
        Debug.Log(Counter + "   enter");
        Counter++;

        for (int i = 0; i < BuyZones.Length; i++)
        {
            if (BuyZones[i].gameObject.activeInHierarchy == false)
            {
                AnimationScale(BuyZones[i].transform);

                Debug.Log(Counter);

                if (Counter > 0)
                    return;
            }
        }
    }

    public override void UnlockNextLevel(BuyZonePresenter buyZone)
    {
    }
}
