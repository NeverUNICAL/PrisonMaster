using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class MoveInQueueState : PrisonerState
{
    [SerializeField] private int _stepState;
    private bool _isfirst = true;

    private void Update()
    {
        if (_stepState == 0)
        {
            if (Prisoner == Prisoner.StartQueue.GetFirstInQueue())
            {
                Prisoner.StartQueue.CanDequeue();
                _isfirst = false;
                return;
            }
        }
        else if (_stepState == 1)
        {
            for (int i = 0; i < Prisoner.QueuePrisoners1.Length; i++)
            {
                if (Prisoner == Prisoner.QueuePrisoners1[i].GetFirstInQueue() && Prisoner.TransferZones1[i].Count > 0)
                {
                    Prisoner.QueuePrisoners1[i].CanDequeue();
                    _isfirst = false;
                    return;
                }
            }
        }
        else if (_stepState == 2)
        {
            for (int i = 0; i < Prisoner.QueuePrisoners2.Length; i++)
            {
                if (Prisoner == Prisoner.QueuePrisoners2[i].GetFirstInQueue() && Prisoner.TransferZones2[i].Count > 0)
                {

                    Prisoner.QueuePrisoners2[i].CanDequeue();
                    _isfirst = false;
                    return;
                }
            }
        }
    }
}
