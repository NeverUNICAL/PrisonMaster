using Agava.IdleGame;
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

    [Header("ZoneSetting")]
    [SerializeField] private Vector3 _scaleTarget;
    [SerializeField] private float _durationForOutlines;

    [Header("Pools")]
    [SerializeField] private QueueHandler _poolAfterShowers;

    [Header("BuyZones")]
    [SerializeField] private LevelBuyZone _secondLevelBuyZone;

    private int _currentArrow = 0;

    private void OnEnable()
    {
        _tutorial.Completed += OnCompleted;
        _poolAfterShowers.PoolPrisonerAdded += OnPoolPrisonerAdded;
        _showerMoneySpawner.MoneySpawned += OnShowerMoneySpawned;
    }

    private void OnDisable()
    {
        _tutorial.Completed -= OnCompleted;
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
        Debug.Log("OnPool");
        AnimationScale(_secondLevelBuyZone.transform);
        ChangeArrow(true);
        _poolAfterShowers.PoolPrisonerAdded -= OnPoolPrisonerAdded;
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
        //if (_playerSavePresenter.IsTutorialCompleted == false)
        //{
            Sequence sequence = DOTween.Sequence();
            buyZone.gameObject.SetActive(true);
            buyZone.transform.localScale = new Vector3(0, 0, 0);
            sequence.Append(buyZone.transform.DOScale(_scaleTarget, _durationForOutlines));
            sequence.Append(buyZone.transform.DOScale(new Vector3(1, 1, 1), _durationForOutlines));
        //}
    }
}
