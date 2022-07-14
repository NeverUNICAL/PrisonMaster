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

    private QueuePrisoners[] _queuePrisoners1;
    private QueuePrisoners[] _queuePrisoners2;
    private QueuePrisoners[] _queuePrisoners3;
    private NavMeshAgent _navMesh;

    private StackView[] _consumerTargetsZone1;
    private StackView[] _consumerTargetsZone2;
    private StackView[] _consumerTargetsZone3;

    private PositionHandler[] _targetPositionZone1;
    private PositionHandler[] _targetPositionZone2;
    private PositionHandler[] _targetPositionZone3;

    private ObjectTransferZone[] _transferZone1;
    private ObjectTransferZone[] _transferZone2;
    private ObjectTransferZone[] _transferZone3;
    private QueuePrisoners _currentQueuePrisoners;
    private bool _isQueueState = false;
    private PositionHandler _positionHandler;

    public PositionHandler PositionHandler => _positionHandler;
    public bool IsQueueState => _isQueueState;
    public QueuePrisoners StartQueue => _startQueue;
    public QueuePrisoners[] QueuePrisoners1 => _queuePrisoners1;
    public QueuePrisoners[] QueuePrisoners2 => _queuePrisoners2;
    public QueuePrisoners[] QueuePrisoners3 => _queuePrisoners3;
    public QueuePrisoners CurrentQueuePrisoners => _currentQueuePrisoners;
    public NavMeshAgent NavMeshAgent => _navMesh;
    public StackView[] ConsumerTargetsZone1 => _consumerTargetsZone1;
    public StackView[] ConsumerTargetsZone2 => _consumerTargetsZone2;
    public StackView[] ConsumerTargetsZone3 => _consumerTargetsZone3;
    public PositionHandler[] PositionHandlersZone1 => _targetPositionZone1;
    public PositionHandler[] PositionHandlersZone2 => _targetPositionZone2;
    public PositionHandler[] PositionHandlersZone3 => _targetPositionZone3;
    public ObjectTransferZone[] TransferZones1 => _transferZone1;
    public ObjectTransferZone[] TransferZones2 => _transferZone2;
    public ObjectTransferZone[] TransferZones3 => _transferZone3;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _consumerTargetsZone1 = new StackView[_consumerZone1.childCount];
        _consumerTargetsZone2 = new StackView[_consumerZone2.childCount];
        _consumerTargetsZone3 = new StackView[_consumerZone3.childCount];
        _targetPositionZone1 = new PositionHandler[_consumerZone1.childCount];
        _targetPositionZone2 = new PositionHandler[_consumerZone2.childCount];
        _targetPositionZone3 = new PositionHandler[_consumerZone3.childCount];
        _transferZone1 = new ObjectTransferZone[_consumerZone1.childCount];
        _transferZone2 = new ObjectTransferZone[_consumerZone2.childCount];
        _transferZone3 = new ObjectTransferZone[_consumerZone3.childCount];
        _queuePrisoners1 = new QueuePrisoners[_consumerZone1.childCount];
        _queuePrisoners2 = new QueuePrisoners[_consumerZone2.childCount];
        _queuePrisoners3 = new QueuePrisoners[_consumerZone3.childCount];

        for (int i = 0; i < _consumerTargetsZone1.Length; i++)
        {
            _queuePrisoners1[i] = _consumerZone1.GetChild(i).GetComponentInChildren<QueuePrisoners>();
            _consumerTargetsZone1[i] = _consumerZone1.GetChild(i).GetComponentInChildren<StackView>();
            _targetPositionZone1[i] = _consumerZone1.GetChild(i).GetComponentInChildren<PositionHandler>();
            _transferZone1[i] = _consumerZone1.GetChild(i).GetComponentInChildren<ObjectTransferZone>();
        }

        for (int i = 0; i < _consumerTargetsZone2.Length; i++)
        {
            _queuePrisoners2[i] = _consumerZone2.GetChild(i).GetComponentInChildren<QueuePrisoners>();
            _consumerTargetsZone2[i] = _consumerZone2.GetChild(i).GetComponentInChildren<StackView>();
            _targetPositionZone2[i] = _consumerZone2.GetChild(i).GetComponentInChildren<PositionHandler>();
            _transferZone2[i] = _consumerZone2.GetChild(i).GetComponentInChildren<ObjectTransferZone>();
        }

        for (int i = 0; i < _consumerTargetsZone3.Length; i++)
        {
            _queuePrisoners3[i] = _consumerZone3.GetChild(i).GetComponentInChildren<QueuePrisoners>();
            _consumerTargetsZone3[i] = _consumerZone3.GetChild(i).GetComponentInChildren<StackView>();
            _targetPositionZone3[i] = _consumerZone3.GetChild(i).GetComponentInChildren<PositionHandler>();
            _transferZone3[i] = _consumerZone3.GetChild(i).GetComponentInChildren<ObjectTransferZone>();
        }
    }

    public void ChangeWorkNavMesh(bool value)
    {
        _navMesh.enabled = value;
    }

    public void SetCurrentQueue(bool value, PositionHandler positionHandler)
    {
        _isQueueState = value;
        _positionHandler = positionHandler;
    }
}
