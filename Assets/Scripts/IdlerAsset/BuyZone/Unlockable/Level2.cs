using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level2 : UnlockableMapZone
{
    [SerializeField] private TrashZone _trashZone;
    [SerializeField] private int _counterForNextLevel = 0;
    [SerializeField] private AssistantsShop _assistantsShop;
    [SerializeField] private LevelBuyZone _fourthLevel;

    private bool _isNextLevelZoneUnlock = false;
    private bool _isFourthLevelOpened = false;
    private bool _isUpgraded = false;
    private bool _isFirst = true;

    public event UnityAction RoomZoneOpened;

    private void OnEnable()
    {
        base.OnEnable();
        _fourthLevel.Unlocked += OnUnlockFourthZone;
        _assistantsShop.CountUpgraded += OnUpgraded;
    }

    private void OnDisable()
    {
        base.OnDisable();
        _fourthLevel.Unlocked -= OnUnlockFourthZone;
        _assistantsShop.CountUpgraded -= OnUpgraded;
    }

    public void Load()
    {
        _isUpgraded = true;
    }

    private void OnUpgraded(int value1, int value2)
    {
        _isUpgraded = true;
    }

    public override void Unlock(BuyZonePresenter buyZone)
    {
        if (IsUnlockRoom)
        {
            if (_isUpgraded)
                RoomEnvirnoment.ChahgeActiveArrow(false);
            else
                RoomEnvirnoment.ChahgeActiveArrow(true);
        }
        else if (IsUnlockRoom == false && Room.gameObject.activeInHierarchy == false)
        {
            AnimationScale(Room.transform);
            RoomZoneOpened?.Invoke();
            AnimationOutlineRoomZone();
        }

        if (_isNextLevelZoneUnlock == false)
        {
            UnlockNextLevelZone();
            _isNextLevelZoneUnlock = true;
        }

        if (_isFourthLevelOpened)
            UnlockBuyZone();
    }

    public override void UnlockNextLevel(BuyZonePresenter buyZone)
    {
        for (int i = 0; i < NextZones.Length; i++)
            NextZones[i].Unlock();

        AnimationScale(_trashZone.transform);
    }

    private void OnUnlockFourthZone(BuyZonePresenter buyZone)
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
    }

    private void UnlockBuyZone()
    {
        if (_isFirst)
        {
            _isFirst = false;
        }
        else
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
