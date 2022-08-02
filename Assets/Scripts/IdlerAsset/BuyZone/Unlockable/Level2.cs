using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : UnlockableMapZone
{
    [SerializeField] private TrashZone _trashZone;
    [SerializeField] private int _counterForNextLevel = 0;

    private bool _isNextLevelUnlock = false;

    public override void Unlock(BuyZonePresenter buyZone)
    {
        int tempCounter = 0;
        int target = 2;
        int targetConutForNextLevel = 2;
        Counter++;
        _counterForNextLevel++;

        for (int i = 0; i < BuyZones.Length; i++)
        {
            if (BuyZones[i].gameObject.activeInHierarchy == false && Counter == target)
            {
                if (tempCounter < target)
                {
                    if (_counterForNextLevel <= targetConutForNextLevel || _isNextLevelUnlock)
                    {
                        AnimationScale(BuyZones[i].transform);
                    }

                    tempCounter++;
                }
                else
                {
                    Counter = 0;
                }

                if (_counterForNextLevel == targetConutForNextLevel && NextLevel.gameObject.activeInHierarchy == false)
                {
                    UnlockNextLevelZone();
                    AnimationScale(Room.transform);
                    AnimationOutlineRoomZone();
                }
            }
        }
    }

    public override void UnlockNextLevel(BuyZonePresenter buyZone)
    {
        int tempCounter = 0;
        int target = 2;

        for (int i = 0; i < NextZones.Length; i++)
        {
            if (NextZones[i].gameObject.activeInHierarchy == false)
            {
                if (tempCounter < target)
                {
                    AnimationScale(_trashZone.transform);
                    AnimationScale(NextZones[i].transform);
                    Unlock(buyZone);
                    tempCounter++;
                    _isNextLevelUnlock = true;
                }
            }
        }
    }
}
