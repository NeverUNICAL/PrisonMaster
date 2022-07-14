using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerFindEmptyQueueTransition : PrisonerTransition
{
    [SerializeField] private int _stepNumber;
    [SerializeField] private PositionHandler _positionAlternative;

    private void Update()
    {
        if (_stepNumber == 0 && Prisoner.IsQueueState == false)
        {   
            if (Prisoner.StartQueue.CanEnqueue(Prisoner))
                NeedTransit = true;
        }
        else if (_stepNumber == 1 && Prisoner.IsQueueState == false)
        {
            for (int i = 0; i < Prisoner.QueuePrisoners1.Length; i++)
            {
                if (Prisoner.QueuePrisoners1[i].gameObject.activeInHierarchy == true && Prisoner.QueuePrisoners1[i].CanEnqueue(Prisoner) == true)
                {
                    if (Prisoner.PositionHandler != null)
                    {
                    NeedTransit = true;
                    return;
                    }
                }
            }
        }
        else if (_stepNumber == 2)
        {
            for (int i = 0; i < Prisoner.QueuePrisoners2.Length; i++)
            {
                if (Prisoner.QueuePrisoners2[i].gameObject.activeInHierarchy == true && Prisoner.QueuePrisoners2[i].CanEnqueue(Prisoner))
                {
                    NeedTransit = true;
                    return;
                }
            }

            if (_positionAlternative.IsEmpty == true)
            {
            NeedAlternativeTransit = true;
            return;
            }
        }
    }
}
