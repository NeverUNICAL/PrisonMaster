using System.Collections;
using TMPro;
using DG.Tweening;
using UnityEngine;
using Agava.IdleGame;

public class PlayersGUI : MonoBehaviour
{
    [SerializeField] private GlobalTutorial _globalTutorial;
    [SerializeField] private CanvasGroup _arrow;
    [SerializeField] private GUIRotator _gUIRotator;
    [SerializeField] private Transform _objectForFollow;
    [SerializeField] private Vector3 _followOffset = new Vector3(0, 0.7f, 0);
    [SerializeField] private float _hiddenDistance = 1f;

    private Transform _target;
    private bool _isArrowEnabled;
    private const float ScaleSize = 2.2f;
    private const float FadeDuration = 0.1f;
    private const float ScaleDuration = 0.2f;
    private float _delayStart = 0.05f;

    private void OnEnable()
    {
        _globalTutorial.PointerShown += OnPointerShown;
        _globalTutorial.PointerHidden += OnPointerHidden;
    }

    private void OnDisable()
    {
        _globalTutorial.PointerShown -= OnPointerShown;
        _globalTutorial.PointerHidden -= OnPointerHidden;
    }

    private void Start()
    {
        Invoke(nameof(DisableArrow), _delayStart);
    }

    private void Update()
    {
        transform.position = _objectForFollow.position + _followOffset;

        if (_target != null)
        {
            if (Vector3.Distance(transform.position, _target.position) < _hiddenDistance && _isArrowEnabled)
                DisableArrow();
            else if (Vector3.Distance(transform.position, _target.position) > _hiddenDistance && _isArrowEnabled == false)
                EnableArrow();
        }
    }

    private void OnPointerShown(Transform transform)
    {
        _target = transform;
        _gUIRotator.ChangeTarget(_target);
        EnableArrow();
    }

    private void OnPointerHidden()
    {
        _target = null;
        _gUIRotator.ChangeTarget(null);
        DisableArrow();
    }

    private void DisableArrow()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_arrow.transform.DOScale(0, ScaleDuration));
        sequence.Append(_arrow.DOFade(0, FadeDuration));
        sequence.Play();

        _gUIRotator.enabled = false;
        _isArrowEnabled = false;
    }

    private void EnableArrow()
    {
        _gUIRotator.enabled = true;
        _isArrowEnabled = true;

        var sequence = DOTween.Sequence();
        sequence.Append(_arrow.DOFade(1, FadeDuration));
        sequence.Append(_arrow.transform.DOScale(ScaleSize, ScaleDuration));
        sequence.Append(_arrow.transform.DOScale(2, ScaleDuration));
        sequence.Play();
    }
}