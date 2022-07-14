using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerExitQueueTransition : PrisonerTransition
{
    [SerializeField] private int _stepNumber;

    private void Update()
    {
        if (_stepNumber == 0)
        {
            for (int i = 0; i < Prisoner.QueuePrisoners1.Length; i++)
            {
                if (Prisoner.QueuePrisoners1[i].gameObject.activeInHierarchy == true && Prisoner.QueuePrisoners1[i].CheckEmptyQueue() == true)
                {
                    if (Prisoner == Prisoner.StartQueue.GetFirstInQueue())
                    {
                    Debug.Log(Prisoner.QueuePrisoners1[i].name);
                        NeedTransit = true;
                        return;
                    }
                }
            }
        }
        else if (_stepNumber == 1)
        {
            for (int i = 0; i < Prisoner.QueuePrisoners1.Length; i++)
            {
                if (Prisoner.QueuePrisoners1[i].gameObject.activeInHierarchy == true)
                {
                    if (Prisoner.TransferZones1[i].Count > 0)
                    {
                        if (Prisoner == Prisoner.QueuePrisoners1[i].GetFirstInQueue())
                        {
                            Prisoner.ChangeWorkNavMesh(true);
                            NeedTransit = true;
                            return;
                        }
                    }
                }
            }
        }
        //else if (_stepNumber == 2)
        //{
        //    for (int i = 0; i < Prisoner.QueuePrisoners2.Length; i++)
        //    {
        //        if (Prisoner == Prisoner.QueuePrisoners2[i].GetFirstInQueue() && Prisoner.TransferZones2[i].Count > 0)
        //        {

        //            Prisoner.QueuePrisoners2[i].CanDequeue();
        //            _isfirst = false;
        //            return;
        //        }
        //    }
        //}
    }
}
