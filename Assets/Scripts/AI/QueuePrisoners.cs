using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QueuePrisoners : MonoBehaviour
{
    [SerializeField] private Transform _startQueue;
    [SerializeField] private float _duration;
    [SerializeField] private float _delay;
    [SerializeField] private bool _startedQueue = false;
    [SerializeField] private float _transitionRange = 0.2f;

    private int _maxCount;
    private Transform[] _wayPoints;
    private PositionHandler[] _positionHandlers;
    private Queue<Prisoner> _prisoners;
    private bool _isLock = true;
    private List<Prisoner> prisoners = new List<Prisoner>();

    public Transform[] WayPoints => _wayPoints;
    public int Count => _prisoners.Count;
    public bool IsLock => _isLock;

    private void Awake()
    {
        _prisoners = new Queue<Prisoner>();
        _wayPoints = new Transform[_startQueue.childCount];
        _positionHandlers = new PositionHandler[_startQueue.childCount];
        _maxCount = _wayPoints.Length;

        for (int i = 0; i < _wayPoints.Length; i++)
        {
            _wayPoints[i] = _startQueue.GetChild(i);
            _positionHandlers[i] = _wayPoints[i].GetComponent<PositionHandler>();
        }
    }

    private void Update()
    {
        if (_positionHandlers[0].IsPrisonerReached)
        {
            _isLock = false;
        }
    }

    public bool CanEnqueue(Prisoner prisoner)
    {
        if (_prisoners.Count < _maxCount)
        {
            AddPrisonerToQueue(prisoner);
            return true;
        }
        else
            return false;
    }

    public bool CanDequeue()
    {
        if (_positionHandlers[0].IsPrisonerReached)
        {
            RemovePrisonerToQueue();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void AddPrisonerToQueue(Prisoner prisoner)
    {
        _prisoners.Enqueue(prisoner);
        prisoners.Add(prisoner);
        for (int i = 0; i < _positionHandlers.Length; i++)
        {
            if (_positionHandlers[i].IsEmpty == true)
            {
                prisoner.SetCurrentQueue(true);
                prisoner.NavMeshAgent.enabled = true;
                prisoner.NavMeshAgent.SetDestination(_positionHandlers[i].transform.position);
                return;
            }
        }
    }

    private void RemovePrisonerToQueue()
    {
        _isLock = true;
        prisoners.Remove(_prisoners.Peek());
        _prisoners.Dequeue().SetCurrentQueue(false);
    }

    public Prisoner GetFirstInQueue()
    {
        if (prisoners.Count == 0)
        {
            return null;
        }
        else
        {
            Prisoner prisoner = prisoners[0];
            if (Vector3.Distance(prisoner.transform.position, _positionHandlers[0].transform.position) < _transitionRange)
            {
                RelocateAllGuests();
                return prisoner;
            }

            return null;
        }
    }

    private void RelocateAllGuests()
    {
        for (int i = 0; i < prisoners.Count; i++)
        {
            prisoners[i].NavMeshAgent.SetDestination(_positionHandlers[i].transform.position);
        }
    }
}
