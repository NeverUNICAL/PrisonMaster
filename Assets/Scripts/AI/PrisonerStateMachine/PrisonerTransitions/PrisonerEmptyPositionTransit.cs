using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;
using UnityEngine.Events;

public class PrisonerEmptyPositionTransit : PrisonerTransition
{
    [SerializeField] private float _startDelay = 0;
    [SerializeField] private float _targetDelay = 3f;

    public event UnityAction<PositionHandler> AcceptTargetPosition;

    private void Update()
    {
        _startDelay = Time.deltaTime;

        if (_startDelay >= _targetDelay)
        {

            for (int i = 0; i < Prisoner.QueuePrisoners1.Length; i++)
            {
                if (Prisoner.QueuePrisoners1[i].CanEnqueue(Prisoner) == true)
                {
                    NeedTransit = true;
                    return;
                }
            }
        }
    }
}
