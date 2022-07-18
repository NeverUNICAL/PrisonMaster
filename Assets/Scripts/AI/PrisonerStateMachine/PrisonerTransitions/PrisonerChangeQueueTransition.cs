using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;

public class PrisonerChangeQueueTransition : PrisonerTransition
{

    private void Update()
    {
        if (Prisoner.StepNumberTransition == -1 && Prisoner.IsQueueState == false)
        {
            if (Prisoner.StartQueue.CanEnqueue(Prisoner))
            {
                Prisoner.SetStepNumber(0);
                NeedTransit = true;
            }
        }
        else if (Prisoner.StepNumberTransition == 0)
        {
            CanMoveWitingArea(Prisoner.StartQueue, Prisoner.QueuePrisoners1, 1);
        }
        else if (Prisoner.StepNumberTransition == 1)
        {
            CanMoveQueueTransition(Prisoner.QueuePrisoners1, Prisoner.QueuePrisoners2, Prisoner.WaitingArea1, Prisoner.CurrentTransferZone, 2, 2);
        }
        else if (Prisoner.StepNumberTransition == 2)
        {
            CanMoveWitingArea(Prisoner.WaitingArea1, Prisoner.QueuePrisoners2, 3);
        }
        else if (Prisoner.StepNumberTransition == 3)
        {
            CanMoveQueueTransition(Prisoner.QueuePrisoners2, Prisoner.QueuePrisoners3, Prisoner.WaitingArea3, Prisoner.CurrentTransferZone, 5, 5);
        }
        else if (Prisoner.StepNumberTransition == 4)
        {
            Debug.Log("finish");
        }
        else if (Prisoner.StepNumberTransition == 5)
        {
            CanMoveWitingArea(Prisoner.WaitingArea3, Prisoner.QueuePrisoners3, 6);
        }
        else if(Prisoner.StepNumberTransition == 6)
        {
            CanMoveQueueTransition(Prisoner.QueuePrisoners3, Prisoner.QueuePrisoners3, Prisoner.WaitingArea2, Prisoner.CurrentTransferZone, 4, 4);
        }
    }

    private void CanMoveQueueTransition(QueuePrisoners[] currentQueue , QueuePrisoners[] targetQueue, QueuePrisoners witingAreaTarget, ObjectTransferZone transferZones, int targetStep, int alternativeTargetStep)
    {
        for (int i = 0; i < targetQueue.Length; i++)
        {
            Debug.Log(targetQueue.Length);
            if (Prisoner.CurrentTransferZone == currentQueue[i].GetComponentInParent<ObjectTransferZone>() && transferZones.Count > 0)
            {
            Debug.Log(currentQueue[i]);
                if (targetQueue[i].gameObject.activeInHierarchy == true && targetQueue[i].CheckEmptyQueue() == true)
                {
                    if (Prisoner == currentQueue[i].GetFirstInQueue())
                    {
                        witingAreaTarget.CanEnqueue(Prisoner);
                        Prisoner.SetStepNumber(targetStep);
                        NeedTransit = true;
                        return;
                    }
                }
                else if (witingAreaTarget.CheckEmptyQueue() == true && Prisoner == currentQueue[i].GetFirstInQueue())
                {
                    witingAreaTarget.CanEnqueue(Prisoner);
                    Prisoner.SetStepNumber(alternativeTargetStep);
                    NeedAlternativeTransit = true;
                    return;
                }
            }
        }
    }

    private void CanMoveWitingArea(QueuePrisoners currentQueue , QueuePrisoners[] targetQueue, int teargetNumberStep)
    {
        for (int i = 0; i < targetQueue.Length; i++)
        {
            Debug.Log(currentQueue);
            Debug.Log(targetQueue[i]);
            if (targetQueue[i].gameObject.activeInHierarchy == true && targetQueue[i].CheckEmptyQueue() == true)
            {
                if (Prisoner == currentQueue.GetFirstInQueue())
                {
                    targetQueue[i].CanEnqueue(Prisoner);
                    Prisoner.SetStepNumber(teargetNumberStep);
                    NeedTransit = true;
                    return;
                }
            }
        }
    }
}
