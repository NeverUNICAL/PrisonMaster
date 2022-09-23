using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;
using Agava.IdleGame.Model;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private RoomBuyZone _openObject;
    [SerializeField] private NormalBuyZonePresenter _showerZone;
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
    [SerializeField] private Bus _bus;

    private bool _isProducerLock = true;

    public event UnityAction Completed;

    private void OnEnable()
    {
        _playerSavePresenter.Loaded += OnSavesLoaded;
        _showerZone.Unlocked += OnUnlock;
        _tutorialMoney.MoneyPullEmpty += OnMoneyPullEmpty;
    }

    private void OnDisable()
    {
        _showerZone.Unlocked -= OnUnlock;
        _playerSavePresenter.Loaded -= OnSavesLoaded;
        _tutorialMoney.MoneyPullEmpty -= OnMoneyPullEmpty;
    }

    private void OnSavesLoaded()
    {
        if (_playerSavePresenter.Money > 0)
        {
            _tutorialMoney.gameObject.SetActive(false);
            OnMoneyPullEmpty();
        }

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
        ChangeActiveArrow(1, 2);
        StartCoroutine(Delay());
        //if (_playerSavePresenter.IsTutorialCompleted == false)
        //{
        //    AnimationOutline(_outlines[1]);
        //    OnStartGame();
        //}
    }

    private void OnStartGame()
    {
        _background.DOFade(0, _duration);

        _bus.gameObject.SetActive(true);
        _prisonersManager.gameObject.SetActive(true);
        //AnimationScale(_openObject.transform);
        AnimationScale(_trashZone.transform);

        gameObject.SetActive(false);
        _playerSavePresenter.SetTutorialComplete();
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
        _bus.gameObject.SetActive(true);
        _prisonersManager.gameObject.SetActive(true);
        _trashZone.gameObject.SetActive(true);
        _openObject.gameObject.SetActive(true);

        gameObject.SetActive(false);
    }

    private void AnimationOutline(Transform outline)
    {
        outline.DOScale(_scaleTarget, _durationForOutlines).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnMoneyPullEmpty()
    {
        AnimationScale(_showerZone.transform);
        ChangeActiveArrow(0, 1);
    }

    private void ChangeActiveArrow(int lockItem, int unlockItem)
    {
        _arrows[lockItem].gameObject.SetActive(false);
        _arrows[unlockItem].gameObject.SetActive(true);
    }

    private IEnumerator Delay()
    {
        yield return new WaitWhile(() => _playerSavePresenter.IsTutorialCompleted);
        if (_playerSavePresenter.IsTutorialCompleted == false)
        {
            Completed?.Invoke();
            //AnimationOutline(_outlines[1]);
            OnStartGame();
        }
    }
}