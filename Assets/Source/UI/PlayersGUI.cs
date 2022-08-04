using System.Collections;
using TMPro;
using DG.Tweening;
using UnityEngine;
using Agava.IdleGame;

public class PlayersGUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _arrow;
    [SerializeField] private GUIRotator _gUIRotator;
    [SerializeField] private Level1 _level1;
    [SerializeField] private Level2 _level2;
    [SerializeField] private RoomBuyZone[] _rooms;
    [SerializeField] private Trigger _hrTrigger;
    [SerializeField] private Trigger _upgradesTrigger;
    [SerializeField] private Transform _objectForFollow;
    [SerializeField] private Transform[] _targetPoints;
    [SerializeField] private Vector3 _followOffset = new Vector3(0, 0.7f, 0);

    private const float ScaleSize = 2.2f;
    private const float FadeDuration = 0.1f;
    private const float ScaleDuration = 0.2f;
    private int _nextPointNumber = 0;
    private float _delayStart = 0.05f;

    private int _arrowRoomUPBuyZone = 0;
    private int _arrowRoomUPShop = 1;
    private int _arrowRoomHRBuyZone = 2;
    private int _arrowRoomHRShop = 3;

    private bool _isUPRoomComplete = false;
    private bool _isLevel2Unlock = false;
    private bool _isHRBuyZoneUnlock = false;
    private bool _isHRRoomComlited = false;

    private void OnEnable()
    {
        _level1.RoomZoneOpened += UnlockLevel1;
        _level2.RoomZoneOpened += UnlockLevel2;

        for (int i = 0; i < _rooms.Length; i++)
            _rooms[i].Unlocked += UnlockRoom;

        _hrTrigger.Enter += OnHREntered;
        _upgradesTrigger.Enter += OnUPEntered;
    }

    private void Start()
    {
        Invoke(nameof(DiasableArrows), _delayStart);
    }

    private void Update()
    {
        transform.position = _objectForFollow.position + _followOffset;
    }

    private void UnlockLevel1()
    {
        EnableArrow();
        ChangeTargetForArrow(_arrowRoomUPBuyZone);
        _level1.RoomZoneOpened -= UnlockLevel1;
    }

    private void UnlockLevel2()
    {
        _isLevel2Unlock = true;
        if (_isUPRoomComplete)
        {
            EnableArrow();
            ChangeTargetForArrow(_arrowRoomHRBuyZone);
        }
        _level2.RoomZoneOpened -= UnlockLevel2;
    }

    private void DisableArrow()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_arrow.transform.DOScale(0, ScaleDuration));
        sequence.Append(_arrow.DOFade(0, FadeDuration));
        sequence.Play();

        _gUIRotator.enabled = false;
    }

    private void OnHREntered()
    {
        _isHRRoomComlited = true;
        if (_isUPRoomComplete)
        {
            DisableArrow();
            ChangeTargetForArrow(4);
        }
        _hrTrigger.Enter -= OnHREntered;
    }

    private void OnUPEntered()
    {
        _isUPRoomComplete = true;

        if (_isHRRoomComlited)
        {
            DisableArrow();
            ChangeTargetForArrow(4);
            _upgradesTrigger.Enter -= OnUPEntered;
            return;
        }

        if (_isHRBuyZoneUnlock)
        {
            ChangeTargetForArrow(_arrowRoomHRShop);
            _upgradesTrigger.Enter -= OnUPEntered;
            return;
        }

        if (_isLevel2Unlock)
        {
            ChangeTargetForArrow(_arrowRoomHRBuyZone);
            _upgradesTrigger.Enter -= OnUPEntered;
            return;
        }

        DisableArrow();
        _upgradesTrigger.Enter -= OnUPEntered;
    }

    private void UnlockRoom(BuyZonePresenter buyZone)
    {
        buyZone.Unlocked -= UnlockRoom;

        if (buyZone == _rooms[0])
            ChangeTargetForArrow(_arrowRoomUPShop);

        if (buyZone == _rooms[1])
        {
            _isHRBuyZoneUnlock = true;

            if (_isUPRoomComplete)
                ChangeTargetForArrow(_arrowRoomHRShop);
        }
    }

    private void ChangeTargetForArrow(int targetNumber)
    {
        for (int i = 0; i < _targetPoints.Length; i++)
            _targetPoints[i].gameObject.SetActive(false);

        if (targetNumber < _targetPoints.Length)
        {
            _targetPoints[targetNumber].gameObject.SetActive(true);
            _gUIRotator.ChangeTarget(_targetPoints[targetNumber]);
        }
    }

    private void DiasableArrows()
    {
        DisableArrow();

        for (int i = 0; i < _targetPoints.Length; i++)
            _targetPoints[i].gameObject.SetActive(false);
    }

    private void EnableArrow()
    {
        _gUIRotator.enabled = true;

        var sequence = DOTween.Sequence();
        sequence.Append(_arrow.DOFade(1, FadeDuration));
        sequence.Append(_arrow.transform.DOScale(ScaleSize, ScaleDuration));
        sequence.Append(_arrow.transform.DOScale(2, ScaleDuration));
        sequence.Play();
    }
}