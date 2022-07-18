using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerDistanceTargetPositionTransition : PrisonerTransition
{
    [SerializeField] private float _transitionRange;

    private void Update()
    {
        if (Vector3.Distance(transform.position, Prisoner.CurrentPositionHandler.transform.position) < _transitionRange)
            NeedTransit = true;
    }
}
