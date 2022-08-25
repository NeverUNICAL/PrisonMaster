using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForCreo
{
    public class QueueFoodZone : QueuePrisoners
    {
        [SerializeField] private QueueCellsZone _cellsZone;
        [SerializeField] private Door _door;
        [SerializeField] private TableFood[] _tableFoods;
        [SerializeField] private float _duration = 1f;

        private Queue<Prisoner> _prisoners = new Queue<Prisoner>();

        private void Awake()
        {
            PointsConteiner = GetComponentInChildren<PointsConteiner>();
            QueuePosition = new PrisonersQueuePoint[PointsConteiner.transform.childCount];

            for (int i = 0; i < QueuePosition.Length; i++)
                QueuePosition[i] = PointsConteiner.transform.GetChild(i).GetComponent<PrisonersQueuePoint>();
        }

        private void OnEnable()
        {
            _door.Opened += OnOpened;

            for (int i = 0; i < _tableFoods.Length; i++)
            {
                //for (int j = 0; j < _tableFoods[i].Points.Length; j++)
                //    _tableFoods[i].Points[j].TriggerIn += OnReachedTable;

                _tableFoods[i].Full += OnFull;
            }

            for (int i = 0; i < QueuePosition.Length; i++)
                QueuePosition[i].TriggerIn += OnTriggerIn;
        }

        private void OnDisable()
        {
            _door.Opened -= OnOpened;

            for (int i = 0; i < _tableFoods.Length; i++)
            {
                //for (int j = 0; j < _tableFoods[i].Points.Length; j++)
                //    _tableFoods[i].Points[j].TriggerIn -= OnReachedTable;

                _tableFoods[i].Full -= OnFull;
            }

            for (int i = 0; i < QueuePosition.Length; i++)
                QueuePosition[i].TriggerIn -= OnTriggerIn;
        }

        private void OnOpened()
        {
            if (_cellsZone.IsFull)
            {
                foreach (var prisoner in _cellsZone.Prisoners)
                {
                    AddQueue(prisoner);
                }
            }
        }

        //private void OnReachedTable(Prisoner targetPrisoner)
        //{
        //    Debug.Log("OnReached");
        //    RotatePrisoner(targetPrisoner);
        //    targetPrisoner.Eating();
        //}

        private void OnTriggerIn(Prisoner targetPrisoner)
        {
            RotatePrisoner(targetPrisoner);
        }

        private void OnFull(TableFood tableFood)
        {
            float temp = 0;
            for (int i = 0; i < tableFood.Points.Length; i++)
            {
                _prisoners.Peek().NavMeshAgent.SetDestination(tableFood.Points[i].transform.position);
                StartCoroutine(PathEnded(_prisoners.Dequeue(), _duration + temp));
                temp += 0.25f;
            }
                Sort(_prisoners);
        }

        protected override void AddQueue(Prisoner targetPrisoner)
        {
            targetPrisoner.NavMeshAgent.SetDestination(QueuePosition[Index].transform.position);
            _prisoners.Enqueue(targetPrisoner);
            Index++;
        }

        private IEnumerator PathEnded(Prisoner prisoner, float duration)
        {
            yield return new WaitForSeconds(duration);

            Debug.Log("PathEnded");
            prisoner.Rotate(LookPoint.transform, _duration / 2);
            prisoner.Eating();
        }
    }
}