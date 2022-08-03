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
    [SerializeField] private Trigger[] _roomZoneTriggers;
    [SerializeField] private Transform _objectForFollow;
    [SerializeField] private Transform[] _targetPoints;
    [SerializeField] private Vector3 _followOffset = new Vector3(0, 0.7f, 0);
    [SerializeField] private PlayerSavePresenter _playerSavePresenter;

    private const float ScaleSize = 2.2f;
    private const float FadeDuration = 0.1f;
    private const float ScaleDuration = 0.2f;
    private int _nextPointNumber = 0;

    private void OnEnable()
    {
        _level1.RoomZoneOpened += EnableArrow;
        _level2.RoomZoneOpened += EnableArrow;

        for (int i = 0; i < _rooms.Length; i++)
            _rooms[i].Unlocked += ChangeTargetForArrow;

        for (int i = 0; i < _roomZoneTriggers.Length; i++)
            _roomZoneTriggers[i].Enter += DisableArrow;
    }

    private void OnDisable()
    {
        _level1.RoomZoneOpened -= EnableArrow;
        _level2.RoomZoneOpened -= EnableArrow;

        for (int i = 0; i < _roomZoneTriggers.Length; i++)
            _roomZoneTriggers[i].Enter -= DisableArrow;
    }

    private void Update()
    {
        transform.position = _objectForFollow.position + _followOffset;
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

    private void DisableArrow()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_arrow.transform.DOScale(0, ScaleDuration));
        sequence.Append(_arrow.DOFade(0, FadeDuration));
        sequence.Play();

        _gUIRotator.enabled = false;

        ChangeTargetForArrow(null);
    }

    private void ChangeTargetForArrow(BuyZonePresenter buyZone)
    {
        _nextPointNumber++;

        for (int i = 0; i < _targetPoints.Length; i++)
        {
            _targetPoints[i].gameObject.SetActive(false);

            if (i == _nextPointNumber)
            {
                _targetPoints[i].gameObject.SetActive(true);
                _gUIRotator.ChangeTarget(_targetPoints[i]);
            }
        }

        if (buyZone != null)
            buyZone.Unlocked -= ChangeTargetForArrow;
    }
}
