using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Prisoner : MonoBehaviour
{
    [SerializeField] private int _reward;
    [SerializeField] private Transform _consumerZone1;
    [SerializeField] private Transform _consumerZone2;
    [SerializeField] private Transform _consumerZone3;
    [SerializeField] private QueuePrisoners _startQueue;
    [SerializeField] private QueuePrisoners _waitingArea1;
    [SerializeField] private QueuePrisoners _waitingArea2;
    [SerializeField] private QueuePrisoners _waitingArea3;
    [SerializeField] private float _delay = 1f;

    private QueuePrisoners _currentQueuePrisoners;
    private QueuePrisoners[] _queuePrisoners1;
    private QueuePrisoners[] _queuePrisoners2;
    private QueuePrisoners[] _queuePrisoners3;
    private NavMeshAgent _navMesh;

    private PositionHandler _currentPositionHandler;
    private PositionHandler[] _targetPositionZone1;
    private PositionHandler[] _targetPositionZone2;
    private PositionHandler[] _targetPositionZone3;

    private ObjectTransferZone _currentTransferZone;
    private ObjectTransferZone[] _transferZone1;
    private ObjectTransferZone[] _transferZone2;
    private ObjectTransferZone[] _transferZone3;
    private bool _isQueueState = false;
    private bool _isCanMove = false;
    private int _stepNumberTransition = -1;

    public int StepNumberTransition => _stepNumberTransition;
    public PositionHandler CurrentPositionHandler => _currentPositionHandler;
    public bool IsQueueState => _isQueueState;
    public QueuePrisoners StartQueue => _startQueue;
    public QueuePrisoners[] QueuePrisoners1 => _queuePrisoners1;
    public QueuePrisoners[] QueuePrisoners2 => _queuePrisoners2;
    public QueuePrisoners[] QueuePrisoners3 => _queuePrisoners3;
    public QueuePrisoners WaitingArea1 => _waitingArea1;
    public QueuePrisoners WaitingArea2 => _waitingArea2;
    public QueuePrisoners WaitingArea3 => _waitingArea3;
    public QueuePrisoners CurrentQueuePrisoners => _currentQueuePrisoners;
    public NavMeshAgent NavMeshAgent => _navMesh;
    public PositionHandler[] PositionHandlersZone1 => _targetPositionZone1;
    public PositionHandler[] PositionHandlersZone2 => _targetPositionZone2;
    public PositionHandler[] PositionHandlersZone3 => _targetPositionZone3;
    public ObjectTransferZone CurrentTransferZone => _currentTransferZone;
    public ObjectTransferZone[] TransferZones1 => _transferZone1;
    public ObjectTransferZone[] TransferZones2 => _transferZone2;
    public ObjectTransferZone[] TransferZones3 => _transferZone3;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _transferZone1 = new ObjectTransferZone[_consumerZone1.childCount];
        _transferZone2 = new ObjectTransferZone[_consumerZone2.childCount];
        _transferZone3 = new ObjectTransferZone[_consumerZone3.childCount];
        _queuePrisoners1 = new QueuePrisoners[_consumerZone1.childCount];
        _queuePrisoners2 = new QueuePrisoners[_consumerZone2.childCount];
        _queuePrisoners3 = new QueuePrisoners[_consumerZone3.childCount];

        for (int i = 0; i < _queuePrisoners1.Length; i++)
        {
            _queuePrisoners1[i] = _consumerZone1.GetChild(i).GetComponentInChildren<QueuePrisoners>();
            _transferZone1[i] = _consumerZone1.GetChild(i).GetComponentInChildren<ObjectTransferZone>();
        }

        for (int i = 0; i < _queuePrisoners2.Length; i++)
        {
            _queuePrisoners2[i] = _consumerZone2.GetChild(i).GetComponentInChildren<QueuePrisoners>();
            _transferZone2[i] = _consumerZone2.GetChild(i).GetComponentInChildren<ObjectTransferZone>();
        }

        for (int i = 0; i < _queuePrisoners3.Length; i++)
        {
            _queuePrisoners3[i] = _consumerZone3.GetChild(i).GetComponentInChildren<QueuePrisoners>();
            _transferZone3[i] = _consumerZone3.GetChild(i).GetComponentInChildren<ObjectTransferZone>();
        }
    }

    public void ChangeWorkNavMesh(bool value)
    {
        _navMesh.enabled = value;
    }

    public void SetCurrentQueue(bool value, PositionHandler targetPositionHandler, ObjectTransferZone targetTransferZone)
    {
        _isQueueState = value;
        _currentPositionHandler = targetPositionHandler;
        _currentTransferZone = targetTransferZone;
    }

    public void SetStepNumber(int value)
    {
        _stepNumberTransition = value;
    }

    public void Move(PositionHandler positionHandler)
    {
        StartCoroutine(DelayMove(_delay, positionHandler));
    }

    private IEnumerator DelayMove(float delay, PositionHandler positionHandler)
    {
        yield return new WaitForSeconds(delay);

        _navMesh.enabled = true;
        _navMesh.SetDestination(positionHandler.transform.position);
    }
}
