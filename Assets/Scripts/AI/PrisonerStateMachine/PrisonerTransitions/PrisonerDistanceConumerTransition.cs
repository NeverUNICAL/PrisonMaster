using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerDistanceConumerTransition : PrisonerTransition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private int _stepNumber;

    private void Update()
    {
        if (_stepNumber == 0)
        {
            for (int i = 0; i < Prisoner.QueuePrisoners1.Length; i++)
            {
                if (Prisoner == Prisoner.QueuePrisoners1[i].GetFirstInQueue() && Prisoner.TransferZones1[i].Count > 0)
                {
                    NeedTransit = true;
                }
            }
        }
    }
}
