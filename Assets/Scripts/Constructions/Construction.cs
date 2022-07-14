using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Construction : MonoBehaviour
{
    [SerializeField] private float _yOnEnablePosition = 1;
    [SerializeField] private Vector3 _doScaleAfterEnable = new Vector3(1.1f, 1.1f, 1.1f);
    [SerializeField] private float _scaleDuration = 1f;
    [SerializeField] private float _backToGroundDuration = 0.2f;

    private void OnEnable()
    {
        Appereance();
    }

    private void Appereance()
    {
        transform.localScale = Vector3.zero;
        transform.position = new Vector3(transform.position.x, _yOnEnablePosition, transform.position.z);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(_doScaleAfterEnable, _scaleDuration).SetEase(Ease.InOutFlash));
        sequence.Append(transform.DOMoveY(0f, _backToGroundDuration).SetEase(Ease.InOutFlash));
        sequence.Join(transform.DOScale(Vector3.one, _backToGroundDuration).SetEase(Ease.InOutFlash));
    }
}
