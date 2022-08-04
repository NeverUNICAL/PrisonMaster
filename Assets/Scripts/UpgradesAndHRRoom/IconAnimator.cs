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
    [SerializeField] private AssistantsShop _assistantsShop;
    [SerializeField] private UpgradesShop _upgradesShop;

    private Tween _tween;
    private int _speedIconNumber = 0;
    private int _countIconNumber = 1;
    private Vector3 _defaultScale;

    private void Awake()
    {
        _defaultScale = transform.localScale;
    }

    private void OnEnable()
    {
        if (_hrTrigger != null)
            _hrTrigger.Enter += OnEnterHRRoom;

        if (_upgradesTrigger != null)
            _upgradesTrigger.Enter += OnEnterUPRoom;

        if (_assistantsShop != null)
            _assistantsShop.CountUpgraded += OnCountUpgrade;

        if (_upgradesShop != null)
            _upgradesShop.SpeedUpgraded += OnSpeedUpgrade;
    }

    private void OnDisable()
    {
        if (_hrTrigger != null)
            _hrTrigger.Enter -= OnEnterHRRoom;

        if (_upgradesTrigger != null)
            _upgradesTrigger.Enter -= OnEnterUPRoom;

        if (_assistantsShop != null)
            _assistantsShop.CountUpgraded -= OnCountUpgrade;

        if (_upgradesShop != null)
            _upgradesShop.SpeedUpgraded -= OnSpeedUpgrade;
    }

    private void OnEnterHRRoom()
    {
        if (_assistantsSave.Count == 0)
            _tween = _icons[_countIconNumber].transform.DOScale(new Vector3(transform.localScale.x + _targetScale, transform.localScale.y + _targetScale, transform.localScale.z), _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnEnterUPRoom()
    {
        if (_playerSavePresenter.SpeedLevel == 0)
            _tween = _icons[_speedIconNumber].transform.DOScale(new Vector3(transform.localScale.x + _targetScale, transform.localScale.y + _targetScale, transform.localScale.z), _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnCountUpgrade(int value, int value1)
    {
        KillTween();
        _icons[_countIconNumber].transform.DOScale(_defaultScale, _duration);
    }

    private void OnSpeedUpgrade(int value, float valu1, int value2)
    {
        KillTween();
        _icons[_speedIconNumber].transform.DOScale(_defaultScale, _duration);
    }

    private void KillTween()
    {
        _tween.Kill();
    }
}
