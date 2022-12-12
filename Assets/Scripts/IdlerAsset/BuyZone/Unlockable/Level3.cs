using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : UnlockableMapZone
{
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
        
        if (NextLevel != null && NextLevel.gameObject.activeInHierarchy == false)
            UnlockNextLevelZone();
    }

    public override void UnlockNextLevel(BuyZonePresenter buyZone)
    {   
    }
}
