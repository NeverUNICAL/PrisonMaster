using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class MoveInQueueState : PrisonerState
{
    [SerializeField] private float _delay;

    private void Update()
    {
        if (Prisoner.StepNumberTransition == 1 || Prisoner.StepNumberTransition == 3 || Prisoner.StepNumberTransition == 5)
            Prisoner.Move(Prisoner.CurrentPositionHandler, _delay);
        else
            Prisoner.Move(Prisoner.CurrentPositionHandler, 0.5f);
    }
}
