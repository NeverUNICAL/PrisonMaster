using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerDistanceConumerTransition : PrisonerTransition
{
    [SerializeField] private float _transitionRange;

    private void Update()
    {
        if (Vector3.Distance(transform.position, ConsumerTarget.transform.position) < _transitionRange)
        {
            NeedTransit = true;
        }
    }
}
