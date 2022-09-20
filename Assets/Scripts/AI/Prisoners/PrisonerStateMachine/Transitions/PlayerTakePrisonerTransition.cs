using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakePrisonerTransition : PrisonerTransition
{
    [SerializeField] private PrisonerProducerZone _prisonerProducerZone;
    [SerializeField] private float _followDistance;
    [SerializeField] private float _speed;

    private new void OnEnable()
    {
        base.OnEnable();
        _prisonerProducerZone.Taked += OnTaked;
    }

    private void OnDisable()
    {
        _prisonerProducerZone.Taked -= OnTaked;
    }

    private void OnTaked(StackPresenter player)
    {
        Prisoner.NavMeshAgent.stoppingDistance = _followDistance;
        Prisoner.NavMeshAgent.speed = _speed;
        Prisoner.SetPosition(player.transform);
        NeedTransit = true;
    }
}
