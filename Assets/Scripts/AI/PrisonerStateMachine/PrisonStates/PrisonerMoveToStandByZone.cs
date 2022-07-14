using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrisonerMoveToStandByZone : PrisonerState
{
    [SerializeField] private Transform _target;
    private NavMeshAgent _navMesh;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _navMesh.SetDestination(_target.position);
        //for (int i = 0; i < PositionHandlers.Length; i++)
        //{
        //    if (PositionHandlers[i].IsEmpty == true)
        //    {
        //        _navMesh.SetDestination(PositionHandlers[i].transform.position);
        //        PositionHandlers[i].SetEmpty(false);
        //        return;
        //    }
        //}
    }
}
