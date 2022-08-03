using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;
using Agava.IdleGame.Model;
using UnityEngine.UI;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private PlayerStackPresenter _player;
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
    [SerializeField] private TutorialMoney _tutorialMoney;

    private bool _isProducerLock = true;

    private void OnEnable()
    {
        _player.AddedForTutorial += OnAdded;
        _player.Removed += OnRemoved;
        _producerZone.Unlocked += OnUnlock;
        _consumerZone.Unlocked += DoFade;
        _tutorialMoney.MoneyPullEmpty += OnMoneyPullEmpty;
    }

    private void OnDisable()
    {
        _producerZone.Unlocked -= OnUnlock;
        _consumerZone.Unlocked -= DoFade;
        _tutorialMoney.MoneyPullEmpty -= OnMoneyPullEmpty;
    }

    private void Start()
    {
        if (_playerSavePresenter.IsTutorialCompleted)
        {
           OnTutorialCompletedOnStart();
        }
        else
        {
            AnimationOutline(_outlines[0]);
        }
    }

    private void OnUnlock(BuyZonePresenter normalBuyZonePresenter)
    {
        if (_playerSavePresenter.IsTutorialCompleted == false)
        {
            AnimationScale(_consumerZone.transform);
            AnimationOutline(_outlines[1]);
            ChangeActiveArrow();
        }
    }

    private void OnStartGame()
    {
        _prisonersManager.gameObject.SetActive(true);
        AnimationScale(_trashZone.transform);

        for (int i = 0; i < _openObjects.Length; i++)
        {
            AnimationScale(_openObjects[i].transform);
            _openObjects[i].gameObject.SetActive(true);
        }
        
        _playerSavePresenter.SetTutorialComplete();
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
            ChangeActiveArrow();
            _isProducerLock = false;
        }
        
    }

    private void AnimationScale(Transform buyZone)
    {
        if (_playerSavePresenter.IsTutorialCompleted == false)
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

    private void OnAdded()
    {
        if (_isProducerLock == false)
        {
        ChangeActiveArrow();
        _player.AddedForTutorial -= OnAdded;
        }
    }

    private void OnRemoved(StackableObject stackable)
    {
        _arrows[0].gameObject.SetActive(false);
        _background.DOFade(0, _duration);
        Invoke(nameof(OnStartGame), _duration);
        _player.Removed -= OnRemoved;
    }

    private void AnimationOutline(Transform outline)
    {
        outline.DOScale(_scaleTarget, _durationForOutlines).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnMoneyPullEmpty()
    {
        if (_playerSavePresenter.IsTutorialCompleted)
        {
            OnTutorialCompletedOnStart();
        }
        else
        {
            AnimationScale(_producerZone.transform);
            ChangeActiveArrow();
        }
    }

    private void ChangeActiveArrow()
    {
        for (int i = 0; i < _arrows.Length; i++)
        {
            if (_arrows[i].gameObject.activeInHierarchy == false)
                _arrows[i].gameObject.SetActive(true);
            else
                _arrows[i].gameObject.SetActive(false);
        }
    }
}