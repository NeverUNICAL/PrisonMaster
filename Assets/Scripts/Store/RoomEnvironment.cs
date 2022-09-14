using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnvironment : MonoBehaviour
{
    [SerializeField] private Transform _environment;
    [SerializeField] private Transform _arrow;

    private Vector3 _scaleTarget = new Vector3(1.2f, 1.2f, 1.2f);
    private Vector3 _defaultBuyZoneScale;
    private float _durationAnimation = 0.5f;

    public void EnableEnvironment()
    {
        AnimationScale(_environment);
    }

    public void ChahgeActiveArrow(bool value)
    {
        _arrow.gameObject.SetActive(value);
    }

    private void AnimationScale(Transform buyZone)
    {
        _defaultBuyZoneScale = buyZone.localScale;
        Sequence sequence = DOTween.Sequence();
        buyZone.gameObject.SetActive(true);
        buyZone.localScale = new Vector3(0, 0, 0);
        sequence.Append(buyZone.DOScale(_scaleTarget, _durationAnimation));

        if (_defaultBuyZoneScale != Vector3.zero)
            sequence.Append(buyZone.DOScale(_defaultBuyZoneScale, _durationAnimation));
        else
            sequence.Append(buyZone.DOScale(Vector3.one, _durationAnimation));
    }
}
