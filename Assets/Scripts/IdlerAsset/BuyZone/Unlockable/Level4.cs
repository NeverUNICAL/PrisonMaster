using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : UnlockableMapZone
{
    [SerializeField] private ObjectCreator _producer;
    [SerializeField] private Transform _prisonersManager;

    private bool _isFirst = true;

    public override void Unlock(BuyZonePresenter buyZone)
    {
        if (_isFirst)
            UnlockNextLevelZone();

        UnlockBuyZone();
    }

    public override void UnlockNextLevel(BuyZonePresenter buyZone)
    {
        for (int i = 0; i < NextZones.Length; i++)
            NextZones[i].Unlock();

        _prisonersManager.gameObject.SetActive(true);
        _producer.gameObject.SetActive(true);
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
