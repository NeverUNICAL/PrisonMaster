using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AllView : MonoBehaviour
{
    [SerializeField] private Transform _followMover;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private ExitClothQueueContainer _exitClothQueue;
    [SerializeField] private float _duration;
    [SerializeField] private float _delay;

    private void OnEnable()
    {
        _exitClothQueue.PrisonerWashEnded += OnPrisonerWashEnded;
    }

    private void OnPrisonerWashEnded()
    {
        StartCoroutine(Delay(_delay));

        _exitClothQueue.PrisonerWashEnded -= OnPrisonerWashEnded;
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
