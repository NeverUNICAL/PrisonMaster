using System;
using TMPro;
using DG.Tweening;
using UnityEngine;
using Agava.IdleGame;

public class PlayersGUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _arrow;
    [SerializeField] private Transform _arrowAlt;
    [SerializeField] private GUIRotator _gUIRotator;
    [SerializeField] private LevelBuyZone _level;
    [SerializeField] private RoomBuyZone _room;
    [SerializeField] private Transform _objectForFollow;
    [SerializeField] private Vector3 _followOffset = new Vector3(0,0.7f,0);

    private const float ScaleSize = 1.1f;
    private const float FadeDuration = 0.1f;
    private const float ScaleDuration = 0.2f;

    private void OnEnable()
    {
        _level.Unlocked += EnableArrow;
        _room.Unlocked += DisableArrow;
    }

    private void Update()
    {
        transform.position = _objectForFollow.position + _followOffset;
    }

    private void EnableArrow(BuyZonePresenter buyZone)
    {
        _gUIRotator.enabled = true;

        var sequence = DOTween.Sequence();
        sequence.Append(_arrow.DOFade(1, FadeDuration));
        sequence.Append(_arrow.transform.DOScale(ScaleSize, ScaleDuration));
        sequence.Append(_arrow.transform.DOScale(1, ScaleDuration));
        sequence.Play();
        _level.Unlocked -= EnableArrow;
    }

    private void DisableArrow(BuyZonePresenter buyZone)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_arrow.transform.DOScale(0, ScaleDuration));
        sequence.Append(_arrow.DOFade(0, FadeDuration));
        sequence.Play();

        _gUIRotator.enabled = false;
        _room.Unlocked -= DisableArrow;
    }
}
