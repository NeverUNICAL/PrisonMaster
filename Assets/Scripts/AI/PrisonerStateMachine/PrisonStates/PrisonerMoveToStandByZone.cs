using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrisonerMoveToStandByZone : PrisonerState
{
    [SerializeField] private PositionHandler _target;

    private void OnEnable()
    {
        Prisoner.SetCurrentQueue(false, _target);
        _target.SetEmpty(false);

        if (Prisoner.NavMeshAgent.enabled == false)
            Prisoner.ChangeWorkNavMesh(true);

        Prisoner.NavMeshAgent.SetDestination(_target.transform.position);
    }
}
