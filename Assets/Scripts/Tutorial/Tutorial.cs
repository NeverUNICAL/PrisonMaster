using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;
using UnityEngine.UI;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private NormalBuyZonePresenter[] _openObjects;
    [SerializeField] private NormalBuyZonePresenter _producerZone;
    [SerializeField] private NormalBuyZonePresenter _consumerZone;
    [SerializeField] private TrashZone _trashZone;
    [SerializeField] private Transform _prisonersManager;
    [SerializeField] private Transform[] _arrows;
    [SerializeField] private Transform[] _outlines;
    [SerializeField] private float _duration;
    [SerializeField] private float _durationForOutlines;
    [SerializeField] private Vector3 _scaleTarget;
    [SerializeField] private Image _background;
    [SerializeField] private PlayerSavePresenter _playerSavePresenter;

    private void OnEnable()
    {
        _producerZone.Unlocked += OnUnlock;
        _consumerZone.Unlocked += DoFade;
    }

    private void OnDisable()
    {
        _producerZone.Unlocked -= OnUnlock;
        _consumerZone.Unlocked -= DoFade;
    }

    private void Start()
    {
        if (_playerSavePresenter.IsTutorialCompleted)
        {
           OnTutorialCompletedOnStart();
        }
        else
        {
            _arrows[0].transform
                .DOMove(
                    new Vector3(_arrows[0].transform.position.x, _arrows[0].transform.position.y - 0.5f,
                        _arrows[0].transform.position.z), _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            _outlines[0].transform.DOScale(_scaleTarget, _durationForOutlines).SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void OnUnlock(BuyZonePresenter normalBuyZonePresenter)
    {
        if (_playerSavePresenter.IsTutorialCompleted)
        {
            
        }
        else
        {
            AnimationScale(_consumerZone.transform);
            _outlines[1].transform.DOScale(_scaleTarget, _durationForOutlines).SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
            _arrows[0].gameObject.SetActive(false);
            _arrows[1].gameObject.SetActive(true);
            _arrows[1].transform
                .DOMove(
                    new Vector3(_arrows[1].transform.position.x, _arrows[1].transform.position.y - 0.5f,
                        _arrows[1].transform.position.z), _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void OnStartGame()
    {
        _playerSavePresenter.SetTutorialComplete();
        _prisonersManager.gameObject.SetActive(true);
        AnimationScale(_trashZone.transform);

        for (int i = 0; i < _openObjects.Length; i++)
        {
            AnimationScale(_openObjects[i].transform);
            _openObjects[i].gameObject.SetActive(true);
        }

        gameObject.SetActive(false);
    }

    private void DoFade(BuyZonePresenter buyZonePresenter)
    {
        if (_playerSavePresenter.IsTutorialCompleted)
        {
            OnTutorialCompletedOnStart();
        }
        else
        {
            _background.DOFade(0,_duration);
            Invoke(nameof(OnStartGame), _duration);
        }
        
    }

    private void AnimationScale(Transform buyZone)
    {
        if (_playerSavePresenter.IsTutorialCompleted)
        {
            
        }
        else
        {
            Sequence sequence = DOTween.Sequence();
            buyZone.gameObject.SetActive(true);
            buyZone.transform.localScale = new Vector3(0, 0, 0);
            sequence.Append(buyZone.transform.DOScale(_scaleTarget, _durationForOutlines));
            sequence.Append(buyZone.transform.DOScale(new Vector3(1, 1, 1), _durationForOutlines));
        }
    }

    private void OnTutorialCompletedOnStart()
    {
        _prisonersManager.gameObject.SetActive(true);
        _trashZone.gameObject.SetActive(true);

        for (int i = 0; i < _openObjects.Length; i++)
        {
            _openObjects[i].gameObject.SetActive(true);
        }
        
        gameObject.SetActive(false);
    }
}