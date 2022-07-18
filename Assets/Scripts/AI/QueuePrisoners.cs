using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Agava.IdleGame;

public class QueuePrisoners : MonoBehaviour
{
    [SerializeField] private Transform _startQueue;
    [SerializeField] private float _duration;
    [SerializeField] private float _delay;
    [SerializeField] private float _transitionRange = 0.2f;
    [SerializeField] private float _speed = 6;

    private ObjectTransferZone _targetTransferZone;
    private int _maxCount;
    private Transform[] _wayPoints;
    private PositionHandler[] _positionHandlers;
    private bool _isLock = true;
    private List<Prisoner> _prisoners = new List<Prisoner>();

    public Transform[] WayPoints => _wayPoints;
    public int Count => _prisoners.Count;
    public bool IsLock => _isLock;

    private void Awake()
    {
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
        if (_prisoners.Count > 0 && _positionHandlers[0].IsPrisonerReached == false)
        {
            RelocateAllGuests();
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

    private void AddPrisonerToQueue(Prisoner prisoner)
    {
        _targetTransferZone = GetComponentInParent<ObjectTransferZone>();
        _prisoners.Add(prisoner);
        for (int i = 0; i < _positionHandlers.Length; i++)
        {
            if (_positionHandlers[i].IsEmpty == true)
            {
                prisoner.SetCurrentQueue(true, _positionHandlers[i], _targetTransferZone);
                _positionHandlers[i].SetEmpty(false);
                return;
            }
        }
    }

    public Prisoner GetFirstInQueue()
    {
        if (_prisoners.Count == 0)
        {
            return null;
        }
        else
        {
            Prisoner prisoner = _prisoners[0];
            if (Vector3.Distance(prisoner.transform.position, _positionHandlers[0].transform.position) < _transitionRange)
            {
                _prisoners[0].SetCurrentQueue(false, prisoner.CurrentPositionHandler, _targetTransferZone);
                _prisoners.RemoveAt(0);
                RelocateAllGuests();
                return prisoner;
            }

            return null;
        }
    }

    private void RelocateAllGuests()
    {

        for (int i = 0; i < _prisoners.Count; i++)
        {
            if (_positionHandlers[i].IsEmpty == true)
            {
                _prisoners[i].Move(_positionHandlers[i]);
                //_prisoners[i].ChangeWorkNavMesh(true);
                //_prisoners[i].NavMeshAgent.SetDestination(_positionHandlers[i].transform.position);
            }
        }
    }

    public bool CheckEmptyQueue()
    {
        if (_prisoners.Count < _maxCount)
            return true;
        else
            return false;
    }
}
