using Agava.IdleGame;
using ForCreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Level1 : UnlockableMapZone
{
    [SerializeField] private TrashZone _trashZone;
    //[SerializeField] private UpgradesShop _upgradesShop;
    [SerializeField] private GlobalTutorial _globalTutorial;

    private bool _isFourthLevelOpened = false;
    //private bool _isUpgraded = false;
    //public event UnityAction RoomZoneOpened;

    private void OnEnable()
    {
        base.OnEnable();
        _globalTutorial.GloalTutorialCompleted += OnGlobalTutorialComplete;
        //_upgradesShop.SpeedUpgraded += OnUpgraded;
    }

    private void OnDisable()
    {
        base.OnDisable();
    }

    //public void Load()
    //{
    //    ChangeUpgradeShopState();
    //}

    //private void OnUpgraded(int value1, float value2, int value3)
    //{
    //    ChangeUpgradeShopState();
    //}

    public override void Unlock(BuyZonePresenter buyZone)
    {
        //if (_isUpgraded)
        //    RoomEnvirnoment.ChahgeActiveArrow(false);
        //else
        //    RoomEnvirnoment.ChahgeActiveArrow(true);

        //if (IsUnlockRoom == false)
        //{
        //    AnimationScale(Room.transform);
        //    AnimationOutlineRoomZone();
        //    RoomZoneOpened?.Invoke();
        //}

        if (_isFourthLevelOpened)
            UnlockBuyZone();
    }

    public override void UnlockNextLevel(BuyZonePresenter buyZone)
    {
        for (int i = 0; i < NextZones.Length; i++)
            NextZones[i].Unlock();

        AnimationScale(_trashZone.transform);
    }

    private void OnGlobalTutorialComplete()
    {
        _isFourthLevelOpened = true;
        UnlockBuyZone();

        _globalTutorial.GloalTutorialCompleted -= OnGlobalTutorialComplete;
    }

    private void UnlockBuyZone()
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

    //private void ChangeUpgradeShopState()
    //{
    //    _isUpgraded = true;
    //    _upgradesShop.SpeedUpgraded -= OnUpgraded;
    //    UnlockNextLevelZone();
    //}
}