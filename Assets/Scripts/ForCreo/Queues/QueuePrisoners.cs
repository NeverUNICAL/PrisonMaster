using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForCreo
{
    public abstract class QueuePrisoners : MonoBehaviour
    {
        [SerializeField] protected Transform LookPoint;

        protected PointsConteiner PointsConteiner;
        protected PrisonersQueuePoint[] QueuePosition;
        protected int Index = 0;

        protected void Sort(Queue<PrisonerForCreo> prisoners)
        {
            int newIndex = 0;
            int first = 0;

            foreach (var prisoner in prisoners)
            {
                if (first > 0)
                {
                    StartCoroutine(DelaySort(prisoner, newIndex));
                    newIndex++;
                }
                else
                {
                    first++;
                }
            }
        }

        protected void RotatePrisoner(PrisonerForCreo targetPrisoner)
        {
            targetPrisoner.Rotate(LookPoint);
        }

        private IEnumerator DelaySort(PrisonerForCreo prisoner, int index)
        {
            yield return new WaitForSeconds(0.5f);

            prisoner.NavMeshAgent.SetDestination(QueuePosition[index].transform.position);
        }

        protected abstract void AddQueue(PrisonerForCreo targetPrisoner);
    }
}