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
    [SerializeField] private RoomBuyZone _upBuyZone;
    [SerializeField] private AssistantsShop _assistantsShop;
    [SerializeField] private Cell _cell;
    [SerializeField] private CellQueueContainer _cellQueueContainer;
    [SerializeField] private TutorialSavePresenter _tutorialSavePresenter;
    [SerializeField] private CameraSwitcher _cameraSwitcher;

    [Header("ZoneSetting")]
    [SerializeField] private Vector3 _scaleTarget;
    [SerializeField] private float _durationForOutlines;

    [Header("Pools")]
    [SerializeField] private QueueHandler[] _targetPools;

    [Header("BuyZones")]
    [SerializeField] private LevelBuyZone[] _levelBuyZones;
    [SerializeField] private NormalBuyZonePresenter _cellBuyZone;

    [Header("Assistants")]
    [SerializeField] private Assistant[] _assistants;
    [SerializeField] private AssistantBuyZone[] _assistantBuyZones;

    private int _currentArrow = 0;
    private int _currentLevelBuyZone = 0;
    private int _currentPool = 0;
    private int _targetStackableLayer = 4;

    private bool _isAssistantsUpgraded = true;
    private bool _isPlayerStayedButton = false;
    private bool _isPoolAddedFirst = true;
    private bool _isLoaded = false;

    public event UnityAction<Transform, bool> PointerShown;
    public event UnityAction PointerHidden;
    public event UnityAction GloalTutorialCompleted;

    private void OnEnable()
    {
        _assistantBuyZones[0].Unlocked += OnUnlockAssistant;
        _hrBuyZone.Unlocked += OnUnlockHRRoom;
        _tutorial.Completed += OnCompleted;
        _tutorialSavePresenter.Loaded += OnLoaded;
        _assistantsShop.CapacityUpgraded += OnCapacityUpgrade;
        _assistantsShop.SpeedUpgraded += OnSpeedUpgrade;
        _exitClothQueue.PrisonerWashEnded += OnPrisonerMovingExit;
        _cell.DoorButtonReached += OnReachedButton;
        _cell.DoorButtonExit += OnButtonExit;
        _cellQueueContainer.PrisonerSendToPool += OnTimerOver;
        _playerStackPresenter.AddedForTutorial += OnAdded;
        _playerStackPresenter.Removed += OnRemoved;
        _showerMoneySpawner.MoneySpawned += OnShowerMoneySpawned;

        for (int i = 0; i < _targetPools.Length; i++)
            _targetPools[i].PoolPrisonerAdded += OnPoolPrisonerAdded;

        for (int i = 0; i < _levelBuyZones.Length; i++)
            _levelBuyZones[i].Unlocked += OnUnlocked;
    }

    private void OnDisable()
    {
        _assistantBuyZones[0].Unlocked -= OnUnlockAssistant;
        _hrBuyZone.Unlocked -= OnUnlockHRRoom;
        _tutorial.Completed -= OnCompleted;
        _tutorialSavePresenter.Loaded -= OnLoaded;
        _assistantsShop.CapacityUpgraded -= OnCapacityUpgrade;
        _assistantsShop.SpeedUpgraded -= OnSpeedUpgrade;
        _exitClothQueue.PrisonerWashEnded -= OnPrisonerMovingExit;
        _cell.DoorButtonReached -= OnReachedButton;
        _cell.DoorButtonExit -= OnButtonExit;
        _cellQueueContainer.PrisonerSendToPool -= OnTimerOver;
        _playerStackPresenter.AddedForTutorial -= OnAdded;
        _playerStackPresenter.Removed -= OnRemoved;
        _showerMoneySpawner.MoneySpawned -= OnShowerMoneySpawned;

        for (int i = 0; i < _targetPools.Length; i++)
            _targetPools[i].PoolPrisonerAdded -= OnPoolPrisonerAdded;

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
        SaveNextArrow();
        _showerMoneySpawner.MoneySpawned -= OnShowerMoneySpawned;
    }

    private void OnPoolPrisonerAdded()
    {
        if (_currentPool == 1 && _isPoolAddedFirst)
        {
            AnimationScale(_assistantBuyZones[0].transform);
            AnimationOutline(_assistantBuyZones[0].Outline);
            AnimationScale(_assistants[0].transform);

            _isAssistantsUpgraded = false;
            _isPoolAddedFirst = false;

            PointerShown?.Invoke(_assistantBuyZones[0].transform, false);
            _tutorialSavePresenter.SetPoollevel(_currentPool);
        }

        if (_isAssistantsUpgraded)
        {
            _levelBuyZones[_currentLevelBuyZone].Unlock();
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
        if (_isLoaded == false)
            ChangeArrows();

        if (_levelBuyZones[0] == buyZone)
        {
            _currentArrow = 2;
            SaveNextArrow();
        }

        if (_levelBuyZones[1] == buyZone)
        {
            _assistantsShop.CapacityUpgraded -= OnCapacityUpgrade;
            _assistantsShop.SpeedUpgraded -= OnSpeedUpgrade;

            AnimationScale(_assistants[0].transform);
            AnimationScale(_assistantBuyZones[0].transform);

            _targetStackableLayer = 5;
            _isAssistantsUpgraded = true;

            DisableArrows();
            _currentArrow = 6;
            SaveNextArrow();
            ChangeActiveArrow(true, true);
        }

        if (_levelBuyZones[2] == buyZone)
        {
            _playerStackPresenter.AddedForTutorial -= OnAdded;
            _playerStackPresenter.Removed -= OnRemoved;

            ChangeStateDoor(false, true, 1);
            if (_arrows[8].gameObject.activeInHierarchy)
                _arrows[8].gameObject.SetActive(false);

            DisableArrows();
            if (_currentArrow < 10)
            {
                if (_cellBuyZone.gameObject.activeInHierarchy == false)
                    _cellBuyZone.gameObject.SetActive(true);

                _currentArrow = 9;
                SaveNextArrow();
            }

            ChangeActiveArrow(true, true);
        }
    }

    private void OnUnlockAssistant(BuyZonePresenter buyZone)
    {
        AnimationScale(_hrBuyZone.transform);
        AnimationOutline(_hrBuyZone.Outline);
        PointerShown?.Invoke(_hrBuyZone.transform, true);
    }

    private void OnUnlockHRRoom(BuyZonePresenter buyZone)
    {
        ChangeActiveArrow(true, false);
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
            SaveNextArrow();
            _targetStackableLayer++;

            _playerStackPresenter.AddedForTutorial += OnAdded;
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

    private void OnPrisonerMovingExit()
    {
        _isLoaded = false;
        AnimationScale(_upBuyZone.transform);
        AnimationOutline(_upBuyZone.Outline);

        AnimationScale(_levelBuyZones[_currentLevelBuyZone].transform);
        ActivateAssistants(false);

        GloalTutorialCompleted?.Invoke();
        _exitClothQueue.PrisonerWashEnded -= OnPrisonerMovingExit;
    }

    private void OnCapacityUpgrade(int value1, int value2, int value3)
    {
        OnAssistantsUpgrade();
    }

    private void OnSpeedUpgrade(int value1, float value2, int value3)
    {
        OnAssistantsUpgrade();
    }

    private void OnAssistantsUpgrade()
    {
        _isLoaded = false;
        _isAssistantsUpgraded = true;

        ChangeArrows();
        PointerShown?.Invoke(_arrows[_currentArrow].transform, false);
        OnPoolPrisonerAdded();

        _assistantsShop.CapacityUpgraded -= OnCapacityUpgrade;
        _assistantsShop.SpeedUpgraded -= OnSpeedUpgrade;
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
                PointerShown?.Invoke(_arrows[_currentArrow], false);
            else
                PointerHidden?.Invoke();
        }
    }

    private void ChangeArrows()
    {
        _isLoaded = false;
        ChangeActiveArrow(false, true);
        _currentArrow++;
        SaveNextArrow();
        ChangeActiveArrow(true, true);

        PointerShown?.Invoke(_arrows[_currentArrow], false);
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
        _cellQueueContainer.PrisonerSendToPool -= OnTimerOver;
        _cell.DoorButtonReached -= OnReachedButton;
        _cell.DoorButtonExit -= OnButtonExit;

        if (_arrows[_currentArrow].gameObject.activeInHierarchy)
            ChangeActiveArrow(false, false);

        _currentArrow++;
        SaveNextArrow();
    }

    private void OnLoaded()
    {
        if (_tutorial.IsTutorialCompleted)
        {
            _currentLevelBuyZone = _tutorialSavePresenter.TutorialLevel;
            _currentPool = _tutorialSavePresenter.PoolLevel;
            _currentArrow = _tutorialSavePresenter.ArrowLevel;

            ChangeStateDoor(false, true, 0);
            _isLoaded = true;

            if (_currentArrow > 9)
            {
                _cameraSwitcher.gameObject.SetActive(false);
                _cellBuyZone.Unlock();
                _currentArrow = 10;
                SaveNextArrow();
                LoadLevelsBuyZones();
                ActivateAssistants(true);
                _hrBuyZone.gameObject.SetActive(true);
                _upBuyZone.gameObject.SetActive(true);
                GloalTutorialCompleted?.Invoke();
                gameObject.SetActive(false);
            }
            else
            {
                PointerShown?.Invoke(_arrows[_currentArrow], false);

                LoadLevelsBuyZones();
                ChangeActiveArrow(true, true);

                for (int i = 0; i < _targetPools.Length; i++)
                {
                    if (i < _currentPool)
                        _targetPools[i].PoolPrisonerAdded -= OnPoolPrisonerAdded;

                    if (i == 0)
                        _showerMoneySpawner.MoneySpawned -= OnShowerMoneySpawned;

                    if (_currentPool == 2 && i == 2)
                    {
                        AnimationScale(_hrBuyZone.transform);
                        AnimationOutline(_hrBuyZone.Outline);

                        _targetStackableLayer++;
                        ChangeActiveArrow(false, false);
                    }
                }
            }
        }
    }

    private void SaveNextArrow()
    {
        _tutorialSavePresenter.SetArrowLevel(_currentArrow);
    }

    private void LoadLevelsBuyZones()
    {
        for (int i = 0; i < _levelBuyZones.Length; i++)
        {
            if (i < _currentLevelBuyZone)
            {
                _levelBuyZones[i].Unlock();
            }
        }
    }

    private void DisableArrows()
    {
        for (int i = 0; i < _arrows.Length; i++)
        {
            if (_arrows[i].gameObject.activeInHierarchy)
                _arrows[i].gameObject.SetActive(false);
        }
    }

    private void ActivateAssistants(bool onLoad)
    {
        int counter = 1;

        for (int i = 0; i < _assistantBuyZones.Length; i++)
        {
            if (onLoad)
            {
                AnimationScale(_assistantBuyZones[i].transform);
                AnimationScale(_assistants[i].transform);
            }
            else
            {
                if (i >= counter)
                {
                    AnimationScale(_assistantBuyZones[i].transform);
                    AnimationScale(_assistants[i].transform);
                }
            }
        }
    }
}