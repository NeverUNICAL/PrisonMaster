using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class MoveInQueueState : PrisonerState
{
    [SerializeField] private int _stepState;

    private void Update()
    {
        //if (Prisoner.NavMeshAgent.enabled == false)
        //    Prisoner.ChangeWorkNavMesh(true);

        //Prisoner.NavMeshAgent.SetDestination(Prisoner.CurrentPositionHandler.transform.position);
        Prisoner.Move(Prisoner.CurrentPositionHandler);
    }
}
