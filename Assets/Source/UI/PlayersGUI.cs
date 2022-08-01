using TMPro;
using DG.Tweening;
using UnityEngine;
using Agava.IdleGame;

public class PlayersGUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _arrow;
    [SerializeField] private GUIRotator _gUIRotator;
    [SerializeField] private LevelBuyZone _level;
    [SerializeField] private RoomBuyZone _room;

    private const float ScaleSize = 1.1f;
    private const float FadeDuration = 0.1f;
    private const float ScaleDuration = 0.2f;

    private void OnEnable()
    {
        _level.Unlocked += EnableArrow;
        _room.Unlocked += DisableArrow;
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

        _room.Unlocked -= DisableArrow;
        _gUIRotator.enabled = false;
    }
}
