using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrisonerMoveToStandByZone : PrisonerState
{
    [SerializeField] private float _delay = 5f;

    private void OnEnable()
    {
        if (Prisoner.StepNumberTransition == 1 || Prisoner.StepNumberTransition == 3 || Prisoner.StepNumberTransition == 5)
            Prisoner.Move(Prisoner.CurrentPositionHandler, _delay);
        else
            Prisoner.Move(Prisoner.CurrentPositionHandler, 0);
    }
}
