using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerMap2 : Prisoner
{
    private Transform _targetPosition;

    public void SetPosition(Transform target)
    {
        _targetPosition = target;
    }
}
