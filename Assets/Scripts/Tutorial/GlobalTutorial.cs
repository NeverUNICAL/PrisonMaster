using Agava.IdleGame;
using Agava.IdleGame.Model;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class GlobalTutorial : MonoBehaviour
{
    [SerializeField] private Tutorial _tutorial;
    [SerializeField] private Door[] _doors;
    [SerializeField] private MoneySpawner _showerMoneySpawner;
    [SerializeField] private ExitClothQueueContainer _exitClothQueue;
    [SerializeField] private Transform[] _arrows;
    [SerializeField] private PlayerStackPresenter _playerStackPresenter;
    [SerializeField] private RoomBuyZone _hrBuyZone;
    [SerializeField] private AssistantsShop _assistantsShop;
    [SerializeField] private RoomBuyZone _upBuyZone;
    [SerializeField] private Cell _cell;
    [SerializeField] private CellQueueContainer _cellQueueContainer;
    [SerializeField] private TutorialSavePresenter _tutorialSavePresenter;

    [Header("ZoneSetting")]
    [SerializeField] private Vector3 _scaleTarget;
    [SerializeField] private float _durationForOutlines;

    [Header("Pools")]
    [SerializeField] private QueueHandler[] _targetPools;

    [Header("BuyZones")]
    [SerializeField] private LevelBuyZone[] _levelBuyZones;

    private int _currentArrow = 0;
    private int _currentLevelBuyZone = 0;
    private int _currentPool = 0;
    private int _targetStackableLayer = 4;
    private bool _isAssistantsUpgraded = true;
    private bool _isPlayerStayedButton = false;
    private bool _isPoolAddedFirst = true;
    private bool _isLoaded = false;

    public event UnityAction<Transform> PointerShown;
    public event UnityAction PointerHidden;

    private void OnEnable()
    {
        _assistantsShop.CountUpgraded += OnAssistantsUpgrade;
        _exitClothQueue.PrisonerWashEnded += OnSuitMoneySpawned;
        _cell.DoorButtonReached += OnReachedButton;
        _cell.DoorButtonExit += OnButtonExit;
        _cellQueueContainer.PrisonerSendToPool += OnTimerOver;
        _hrBuyZone.Unlocked += OnUnlocked;
        _playerStackPresenter.AddedForTutorial += OnAdded;
        _playerStackPresenter.Removed += OnRemoved;
        _tutorial.Completed += OnCompleted;
        _showerMoneySpawner.MoneySpawned += OnShowerMoneySpawned;
        _tutorialSavePresenter.Loaded += OnLoaded;

        for (int i = 0; i < _targetPools.Length; i++)
            _targetPools[i].PoolPrisonerAdded += OnPoolPrisonerAdded;

        for (int i = 0; i < _levelBuyZones.Length; i++)
            _levelBuyZones[i].Unlocked += OnUnlocked;
    }

    private void OnDisable()
    {
        _hrBuyZone.Unlocked -= OnUnlocked;
        _tutorial.Completed -= OnCompleted;
        _tutorialSavePresenter.Loaded -= OnLoaded;

        for (int i = 0; i < _levelBuyZones.Length; i++)
            _levelBuyZones[i].Unlocked -= OnUnlocked;
    }

    private void OnCompleted()
    {
        ChangeStateDoor(true, false, 0);
        ChangeStateDoor(true, false, 1);
        ChangeActiveArrow(true, false);
    }

    private void OnShowerMoneySpawned()
    {
        ChangeStateDoor(false, true, 0);
        ChangeActiveArrow(false, false);
        _currentArrow++;
        SaveCurrentArrow();
        _showerMoneySpawner.MoneySpawned -= OnShowerMoneySpawned;
    }

    private void OnPoolPrisonerAdded()
    {
        if (_currentPool == 1 && _isPoolAddedFirst)
        {
            AnimationScale(_hrBuyZone.transform);
            AnimationOutline(_hrBuyZone.Outline);
            PointerShown?.Invoke(_hrBuyZone.transform);
            _isAssistantsUpgraded = false;
            _isPoolAddedFirst = false;
            _tutorialSavePresenter.SetPoollevel(_currentPool);
        }

        Debug.Log("OnPool");
        if (_isAssistantsUpgraded)
        {
            Debug.Log("Enter");
            AnimationScale(_levelBuyZones[_currentLevelBuyZone].transform);
            AnimationOutline(_levelBuyZones[_currentLevelBuyZone].Outline);
            _currentLevelBuyZone++;
            _tutorialSavePresenter.SetTutorialLevel();

            ChangeActiveArrow(true, false);

            _targetPools[_currentPool].PoolPrisonerAdded -= OnPoolPrisonerAdded;
            _currentPool++;
            _tutorialSavePresenter.SetPoollevel(_currentPool);
        }
    }

    private void OnUnlocked(BuyZonePresenter buyZone)
    {
        if (_hrBuyZone == buyZone)
        {
            if (_currentArrow != 5)
                ChangeActiveArrow(true, false);
        }
        else
        {
            if (_isLoaded == false)
                ChangeArrows();

            if (_levelBuyZones[1] == buyZone)
            {
                _assistantsShop.CountUpgraded -= OnAssistantsUpgrade;

                if (_levelBuyZones[2].gameObject.activeInHierarchy == false)
                {
                    if (_isLoaded == false)
                    {
                        _playerStackPresenter.AddedForTutorial += OnAdded;
                        _playerStackPresenter.Removed += OnRemoved;
                    }

                    _isAssistantsUpgraded = true;
                    _currentArrow = 6;
                    SaveCurrentArrow();
                    ChangeActiveArrow(true, true);
                }
            }
        }

        if (_levelBuyZones[2] == buyZone)
        {
            _playerStackPresenter.AddedForTutorial -= OnAdded;
            _playerStackPresenter.Removed -= OnRemoved;

            ChangeStateDoor(false, true, 1);
            if (_arrows[8].gameObject.activeInHierarchy)
                _arrows[8].gameObject.SetActive(false);

            _currentArrow = 9;
            SaveCurrentArrow();
            ChangeActiveArrow(true, true);
        }
    }

    private void OnAdded(StackableObject stackable)
    {
        if (stackable.Layer == _targetStackableLayer)
        {
            _isLoaded = false;
            ChangeArrows();

            _playerStackPresenter.AddedForTutorial -= OnAdded;
        }
    }

    private void OnRemoved(StackableObject stackable)
    {
        if (stackable.Layer == _targetStackableLayer)
        {
            _isLoaded = false;
            ChangeActiveArrow(false, false);
            _currentArrow++;
            SaveCurrentArrow();
            _targetStackableLayer++;
            _playerStackPresenter.Removed -= OnRemoved;
        }
    }

    private void OnTimerOver()
    {
        if (_isPlayerStayedButton)
            ActivateCameraFollowPrisoner();
        else
            StartCoroutine(CheckStayedPlayerButton());
    }

    private void OnReachedButton()
    {
        if (_isPlayerStayedButton == false)
            _isPlayerStayedButton = true;
    }

    private void OnButtonExit()
    {
        if (_isPlayerStayedButton == true)
            _isPlayerStayedButton = false;
    }

    private void OnSuitMoneySpawned()
    {
        _isLoaded = false;
        AnimationScale(_upBuyZone.transform);
        AnimationOutline(_upBuyZone.Outline);

        AnimationScale(_levelBuyZones[_currentLevelBuyZone].transform);

        _exitClothQueue.PrisonerWashEnded -= OnSuitMoneySpawned;
    }

    private void OnAssistantsUpgrade(int value1, int value2)
    {
        _isLoaded = false;
        _isAssistantsUpgraded = true;

        ChangeArrows();
        OnPoolPrisonerAdded();

        _assistantsShop.CountUpgraded -= OnAssistantsUpgrade;
    }

    private void ChangeStateDoor(bool doorObstacleValue, bool triggerValue, int index)
    {
        if (index == 0)
            _doors[index].DoorObstacle.enabled = doorObstacleValue;

        _doors[index].Trigger.gameObject.SetActive(triggerValue);
    }

    private void ChangeActiveArrow(bool value, bool isChangeArrowsMethod)
    {
        _arrows[_currentArrow].gameObject.SetActive(value);

        if (isChangeArrowsMethod == false)
        {
            if (value)
                PointerShown?.Invoke(_arrows[_currentArrow]);
            else
                PointerHidden?.Invoke();
        }
    }

    private void ChangeArrows()
    {
        _isLoaded = false;
        ChangeActiveArrow(false, true);
        _currentArrow++;
        SaveCurrentArrow();
        ChangeActiveArrow(true, true);

        PointerShown?.Invoke(_arrows[_currentArrow]);
    }

    private void AnimationScale(Transform buyZone)
    {
        Sequence sequence = DOTween.Sequence();
        buyZone.gameObject.SetActive(true);
        buyZone.transform.localScale = new Vector3(0, 0, 0);
        sequence.Append(buyZone.transform.DOScale(_scaleTarget, _durationForOutlines));
        sequence.Append(buyZone.transform.DOScale(new Vector3(1, 1, 1), _durationForOutlines));
    }

    private void AnimationOutline(Transform outline)
    {
        outline.DOScale(_scaleTarget, _durationForOutlines).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private IEnumerator CheckStayedPlayerButton()
    {
        WaitWhile wait = new WaitWhile(() => _isPlayerStayedButton == false);

        yield return wait;

        ActivateCameraFollowPrisoner();
    }

    private void ActivateCameraFollowPrisoner()
    {
        _cell.DoorButtonReached -= OnReachedButton;
        _cell.DoorButtonExit -= OnButtonExit;

        ChangeActiveArrow(false, false);
    }

    private void OnLoaded()
    {
        if (_tutorial.IsTutorialCompleted)
        {
            _isLoaded = true;
            _currentLevelBuyZone = _tutorialSavePresenter.TutorialLevel;
            _currentPool = _tutorialSavePresenter.PoolLevel;
            _currentArrow = _tutorialSavePresenter.ArrowLevel;

            ChangeStateDoor(false, true, 0);
            for (int i = 0; i < _levelBuyZones.Length; i++)
            {
                if (i < _currentLevelBuyZone)
                {
                    AnimationScale(_levelBuyZones[i].transform);
                    AnimationOutline(_levelBuyZones[i].Outline);
                }
            }

            ChangeActiveArrow(true, true);

            for (int i = 0; i < _targetPools.Length; i++)
            {
                if (i < _currentPool)
                    _targetPools[i].PoolPrisonerAdded -= OnPoolPrisonerAdded;

                if (i == 0)
                    _showerMoneySpawner.MoneySpawned -= OnShowerMoneySpawned;

                if (i == 2)
                {
                    AnimationScale(_hrBuyZone.transform);
                    AnimationOutline(_hrBuyZone.Outline);

                    _targetStackableLayer++;
                    ChangeActiveArrow(false, false);
                }
            }
        }
    }

    private void SaveCurrentArrow()
    {
        _tutorialSavePresenter.SetArrowLevel(_currentArrow);
    }
}