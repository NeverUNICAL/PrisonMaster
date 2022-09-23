using Agava.IdleGame;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    [SerializeField] private NavMeshObstacle _door;
    [SerializeField] private Trigger _trigger;
    [SerializeField] private Vector3 _targetPosition = new Vector3(2.2f, 0, 0);
    [SerializeField] private Vector3 _defaultPosition = new Vector3(0.2f, 0, 0);
    [SerializeField] private float _duration = 0.5f;

    public NavMeshObstacle DoorObstacle => _door;
    public Trigger Trigger => _trigger;

    private void OnEnable()
    {
        _trigger.Enter += Open;
        _trigger.Exit += Close;
    }

    private void OnDisable()
    {
        _trigger.Enter -= Open;
        _trigger.Exit -= Close;
    }

    private void Open()
    {
        Move(_targetPosition, _duration);
    }

    private void Close()
    {
        Move(_defaultPosition, _duration);
    }

    private void Move(Vector3 target, float duration)
    {
        _door.transform.DOLocalMove(target, duration);
    }
}
