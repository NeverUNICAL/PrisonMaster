using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrisonerMoveToConsumer : PrisonerState
{
    private void OnEnable()
    {
        Prisoner.ChangeWorkNavMesh(true);

        for (int i = 0; i < Prisoner.CurrentQueuePrisoners.WayPoints.Length; i++)
        {
                Prisoner.NavMeshAgent.SetDestination(Prisoner.CurrentQueuePrisoners.WayPoints[Prisoner.CurrentQueuePrisoners.Count - 1].position);
        }
    }
}
