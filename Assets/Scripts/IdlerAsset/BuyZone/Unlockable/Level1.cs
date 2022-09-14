using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Level1 : UnlockableMapZone
{
    [SerializeField] private TrashZone _trashZone;
    [SerializeField] private UpgradesShop _upgradesShop;

    private bool _isUpgraded = false;
    public event UnityAction RoomZoneOpened;

    private void OnEnable()
    {
        base.OnEnable();
        _upgradesShop.SpeedUpgraded += OnUpgraded;
    }

    private void OnDisable()
    {
        base.OnDisable();
        _upgradesShop.SpeedUpgraded -= OnUpgraded;
    }

    public void Load()
    {
        _isUpgraded = true;
        Unlock(null);
    }

    private void OnUpgraded(int value1, float value2, int value3)
    {
        Counter++;
        _isUpgraded = true;
        Unlock(null);
    }

    public override void Unlock(BuyZonePresenter buyZone)
    {
        int tempCounter = 0;
        int target = 2;
        Counter++;

        if (_isUpgraded)
            RoomEnvirnoment.ChahgeActiveArrow(false);
        else
            RoomEnvirnoment.ChahgeActiveArrow(true);

        if (IsUnlockRoom == false)
        {
            AnimationScale(Room.transform);
            AnimationOutlineRoomZone();
            RoomZoneOpened?.Invoke();
        }

        for (int i = 0; i < BuyZones.Length; i++)
        {
            if (BuyZones[i].gameObject.activeInHierarchy == false && Counter == target)
            {
                if (_isUpgraded)
                {
                    if (NextLevel != null && NextLevel.gameObject.activeInHierarchy == false)
                        UnlockNextLevelZone();

                    if (tempCounter < target)
                    {
                        AnimationScale(BuyZones[i].transform);
                    }

                    tempCounter++;
                }
            }
        }

        if (Counter == target)
            Counter = 0;
    }

    public override void UnlockNextLevel(BuyZonePresenter buyZone)
    {
        for (int i = 0; i < NextZones.Length; i++)
            NextZones[i].Unlock();

        AnimationScale(_trashZone.transform);
    }
}