using Agava.IdleGame;
using Agava.IdleGame.Model;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GlobalTutorial : MonoBehaviour
{
    [SerializeField] private Tutorial _tutorial;
    [SerializeField] private Door _door;
    [SerializeField] private MoneySpawner _showerMoneySpawner;
    [SerializeField] private Transform[] _arrows;
    [SerializeField] private PlayerStackPresenter _playerStackPresenter;
    [SerializeField] private RoomBuyZone _hrBuyZone;

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
    private bool _isWashersZone = true;

    private void OnEnable()
    {
        _hrBuyZone.Unlocked += OnUnlocked;
        _playerStackPresenter.AddedForTutorial += OnAdded;
        _playerStackPresenter.Removed += OnRemoved;
        _tutorial.Completed += OnCompleted;
        _showerMoneySpawner.MoneySpawned += OnShowerMoneySpawned;

        for (int i = 0; i < _targetPools.Length; i++)
            _targetPools[i].PoolPrisonerAdded += OnPoolPrisonerAdded;

        for (int i = 0; i < _levelBuyZones.Length; i++)
            _levelBuyZones[i].Unlocked += OnUnlocked;
    }

    private void OnDisable()
    {
        _hrBuyZone.Unlocked -= OnUnlocked;
        _tutorial.Completed -= OnCompleted;

        for (int i = 0; i < _levelBuyZones.Length; i++)
            _levelBuyZones[i].Unlocked -= OnUnlocked;
    }

    private void OnCompleted()
    {
        ChangeStateDoor(true, false);
        ChangeArrow(true);
    }

    private void OnShowerMoneySpawned()
    {
        ChangeStateDoor(false, true);
        ChangeArrow(false);
        _currentArrow++;
        _showerMoneySpawner.MoneySpawned -= OnShowerMoneySpawned;
    }

    private void OnPoolPrisonerAdded()
    {
        AnimationScale(_levelBuyZones[_currentLevelBuyZone].transform);
        AnimationOutline(_levelBuyZones[_currentLevelBuyZone].Outline);
        _currentLevelBuyZone++;

        ChangeArrow(true);

        _targetPools[_currentPool].PoolPrisonerAdded -= OnPoolPrisonerAdded;
        _currentPool++;
    }

    private void OnUnlocked(BuyZonePresenter buyZone)
    {
        ChangeArrow(false);
        _currentArrow++;
        ChangeArrow(true);
    }

    private void OnAdded()
    {
        ChangeArrow(false);
        _currentArrow++;
        ChangeArrow(true);

        _playerStackPresenter.AddedForTutorial -= OnAdded;
    }

    private void OnRemoved(StackableObject stackable)
    {
        if (_isWashersZone)
        {
            AnimationScale(_hrBuyZone.transform);
            AnimationOutline(_hrBuyZone.Outline);
            _isWashersZone = false;
        }

        ChangeArrow(false);
        _currentArrow++;

        _playerStackPresenter.Removed -= OnRemoved;
    }

    private void ChangeStateDoor(bool doorObstacleValue, bool triggerValue)
    {
        _door.DoorObstacle.enabled = doorObstacleValue;
        _door.Trigger.gameObject.SetActive(triggerValue);
    }

    private void ChangeArrow(bool value)
    {
        _arrows[_currentArrow].gameObject.SetActive(value);
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
}
