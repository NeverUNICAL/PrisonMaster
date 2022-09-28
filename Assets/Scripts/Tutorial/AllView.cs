using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AllView : MonoBehaviour
{
    [SerializeField] private Transform _followMover;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private GlobalTutorial _globalTutorial;
    [SerializeField] private float _duration;
    [SerializeField] private float _delay;

    private void OnEnable()
    {
        _globalTutorial.GloalTutorialCompleted += OnGloalTutorialCompleted;
    }

    private void OnGloalTutorialCompleted()
    {
        StartCoroutine(Delay(_delay));

        _globalTutorial.GloalTutorialCompleted -= OnGloalTutorialCompleted;
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
