using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Agava.IdleGame;

public class IconAnimator : MonoBehaviour
{
    [SerializeField] private float _targetScale;
    [SerializeField] private float _duration;
    [SerializeField] private AssistantsSavePresenter _assistantsSave;
    [SerializeField] private PlayerSavePresenter _playerSavePresenter;

    [SerializeField] private RectTransform[] _icons;
    [SerializeField] private Trigger _hrTrigger;
    [SerializeField] private Trigger _upgradesTrigger;

    private Tween _tween;
    private int _speedIconNumber = 0;
    private int _countIconNumber = 1;

    private void OnEnable()
    {
        if (_hrTrigger != null)
            _hrTrigger.Enter += OnEnterHRRoom;

        if (_upgradesTrigger != null)
        {
            _upgradesTrigger.Enter += OnEnterUPRoom;
            Debug.Log("подписался");
        }
    }

    private void OnDisable()
    {
        if (_hrTrigger != null)
            _hrTrigger.Enter -= OnEnterHRRoom;

        if (_upgradesTrigger != null)
            _upgradesTrigger.Enter -= OnEnterUPRoom;

        _tween.Kill();
    }

    private void OnEnterHRRoom()
    {
        if (_assistantsSave.Count == 0)
            _tween = _icons[_countIconNumber].transform.DOScale(new Vector3(transform.localScale.x + _targetScale, transform.localScale.y + _targetScale, transform.localScale.z), _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnEnterUPRoom()
    {
        Debug.Log("enter");
        if (_playerSavePresenter.SpeedLevel == 0)
            _tween = _icons[_speedIconNumber].transform.DOScale(new Vector3(transform.localScale.x + _targetScale, transform.localScale.y + _targetScale, transform.localScale.z), _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
