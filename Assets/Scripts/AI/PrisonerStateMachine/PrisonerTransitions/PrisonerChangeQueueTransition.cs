using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;

public class PrisonerChangeQueueTransition : PrisonerTransition
{
    [SerializeField] private float _targetTimer = 30f;

    private float _currentTimer = 0f;
    private bool _isFirst = false;

    private void Update()
    {
        _currentTimer += Time.deltaTime;
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
            CanMoveWitingArea(Prisoner.StartQueue, Prisoner.QueuePrisoners1, 1, 1);
        }
        else if (Prisoner.StepNumberTransition == 1)
        {
            CanMoveQueueTransition(Prisoner.QueuePrisoners1, Prisoner.QueuePrisoners2, Prisoner.WaitingArea1, Prisoner.CurrentTransferZone, 2, 2);
        }
        else if (Prisoner.StepNumberTransition == 2)
        {
            CanMoveWitingArea(Prisoner.WaitingArea1, Prisoner.QueuePrisoners2, 3, 3);
        }
        else if (Prisoner.StepNumberTransition == 3)
        {
            CanMoveQueueTransition(Prisoner.QueuePrisoners2, Prisoner.QueuePrisoners3, Prisoner.WaitingArea2, Prisoner.CurrentTransferZone, 4, 4);
        }
        else if (Prisoner.StepNumberTransition == 4)
        {
            if (_isFirst == false)
            {
                _currentTimer = 0f;
                _isFirst = true;
            }
            CanMoveWitingArea(Prisoner.WaitingArea2, Prisoner.QueuePrisoners3, 5, 6);
        }
        else if (Prisoner.StepNumberTransition == 5)
        {
            CanMoveQueueTransition(Prisoner.QueuePrisoners3, Prisoner.QueuePrisoners3, Prisoner.WaitingArea3, Prisoner.CurrentTransferZone, 6, 6);
        }
        else if (Prisoner.StepNumberTransition == 6)
        {
            Debug.Log("finish");
        }
    }

    private void CanMoveQueueTransition(QueuePrisoners[] currentQueue, QueuePrisoners[] targetQueue, QueuePrisoners witingAreaTarget, StackPresenter transferZones, int targetStep, int alternativeTargetStep)
    {
        for (int i = 0; i < currentQueue.Length; i++)
        {
            if (currentQueue[i].gameObject.activeInHierarchy == true)
            {
                StackPresenter stackPresenter = currentQueue[i].GetComponentInParent<Shop>().GetStackPresenter();
                if (Prisoner.CurrentTransferZone == stackPresenter && transferZones.Count > 2)
                {
                    Debug.Log("step0");
                    if (targetQueue[i] != null && targetQueue[i].gameObject.activeInHierarchy == true && targetQueue[i].CheckEmptyQueue() == true)
                    {
                        Debug.Log("step1");
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
    }

    private void CanMoveWitingArea(QueuePrisoners currentQueue, QueuePrisoners[] targetQueue, int teargetNumberStep, int alterntiveNumberStep)
    {
        for (int i = 0; i < targetQueue.Length; i++)
        {
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
            else if (Prisoner.StepNumberTransition == 4 && _currentTimer > _targetTimer)
            {
                if (Prisoner == currentQueue.GetFirstInQueue())
                {
                    Prisoner.WaitingArea3.CanEnqueue(Prisoner);
                    Prisoner.SetStepNumber(alterntiveNumberStep);
                    NeedAlternativeTransit = true;
                    return;
                }
            }
        }
    }
}
