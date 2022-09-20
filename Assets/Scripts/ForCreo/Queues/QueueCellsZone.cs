using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForCreo
{
    public class QueueCellsZone : QueuePrisoners
    {
        //[SerializeField] private CellsConteiner _cellsConteiner;

        //private Queue<Prisoner> _prisoners = new Queue<Prisoner>();
        //private bool _isFull = false;

        //public Queue<Prisoner> Prisoners => _prisoners;
        //public bool IsFull => _isFull;

        //private void Awake()
        //{
        //    PointsConteiner = GetComponentInChildren<PointsConteiner>();
        //    QueuePosition = new PrisonersQueuePoint[PointsConteiner.transform.childCount];

        //    for (int i = 0; i < QueuePosition.Length; i++)
        //        QueuePosition[i] = PointsConteiner.transform.GetChild(i).GetComponent<PrisonersQueuePoint>();
        //}

        //private void OnEnable()
        //{
        //    for (int i = 0; i < _cellsConteiner.Cells.Length; i++)
        //        _cellsConteiner.Cells[i].DoorOpened += OnDoorOpened;

        //    for (int i = 0; i < QueuePosition.Length; i++)
        //        QueuePosition[i].TriggerIn += OntriggerIn;
        //}

        //private void OnDisable()
        //{
        //    for (int i = 0; i < _cellsConteiner.Cells.Length; i++)
        //        _cellsConteiner.Cells[i].DoorOpened -= OnDoorOpened;

        //    for (int i = 0; i < QueuePosition.Length; i++)
        //        QueuePosition[i].TriggerIn -= OntriggerIn;
        //}

        //private void OnDoorOpened(Cell cell)
        //{
            
        //    for (int i = 0; i < cell.Prisoners.Length; i++)
        //    {
        //        AddQueue(cell.Prisoners[i]);
        //    }

        //    if (Index >= QueuePosition.Length)
        //        _isFull = true;
        //}

        //private void OntriggerIn(Prisoner targetPrisoner)
        //{
        //    RotatePrisoner(targetPrisoner);
        //}

        //protected override void AddQueue(Prisoner targetPrisoner)
        //{
        //    targetPrisoner.NavMeshAgent.SetDestination(QueuePosition[Index].transform.position);
        //    _prisoners.Enqueue(targetPrisoner);
        //    Index++;
        //}
    }
}