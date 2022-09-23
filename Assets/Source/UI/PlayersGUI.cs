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

    private const float ScaleSize = 2.2f;
    private const float FadeDuration = 0.1f;
    private const float ScaleDuration = 0.2f;

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

    private void Update()
    {
            transform.position = _objectForFollow.position + _followOffset;
    }

    private void OnPointerShown(Transform transform)
    {
        _gUIRotator.ChangeTarget(transform);
        EnableArrow();
    }

    private void OnPointerHidden()
    {
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
    }

    private void EnableArrow()
    {
        Debug.Log("Enablearrow");
        _gUIRotator.enabled = true;

        var sequence = DOTween.Sequence();
        sequence.Append(_arrow.DOFade(1, FadeDuration));
        sequence.Append(_arrow.transform.DOScale(ScaleSize, ScaleDuration));
        sequence.Append(_arrow.transform.DOScale(2, ScaleDuration));
        sequence.Play();
    }
}