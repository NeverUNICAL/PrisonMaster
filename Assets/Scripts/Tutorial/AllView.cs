using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AllView : MonoBehaviour
{
    [SerializeField] private Transform _followMover;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private MoneySpawner _suitCabinetMoneySpawner;
    [SerializeField] private float _duration;
    [SerializeField] private float _delay;

    private void OnEnable()
    {
        _suitCabinetMoneySpawner.MoneySpawned += OnMoneySpawned;
    }

    private void OnMoneySpawned()
    {
        StartCoroutine(Delay(_delay));

        _suitCabinetMoneySpawner.MoneySpawned -= OnMoneySpawned;
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Move(_targetPosition.position, _duration);
    }

    private void Move(Vector3 targetPosition, float duration)
    {
        _followMover.DOMove(targetPosition, duration).SetEase(Ease.Linear);
    }
}
