using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(PlayerStackPresenter))]
public class Assistant : MonoBehaviour
{
    [SerializeField] private ProdusersContainer producersContainer;
    [SerializeField] private ConsumersContainer consumersContainer;
    
    [Header("OptionsForUpgrade")]
    [SerializeField] private float _speed;

    private NavMeshAgent _navMeshAgent;
    private PlayerStackPresenter _stackPresenter;
    private StackView[] _producers;
    private StackView[] _consumers;
    private StackView _assistantStack;
    private AssistantStateMachine _stateMachine;

    private List<StackPresenter> _producerItemCreators = new List<StackPresenter>();
    private List<StackPresenter> _consumersItemCreators = new List<StackPresenter>();
    private Transform _targetTransform;

    public float Speed => _speed;
    public int Capacity => _stackPresenter.Capacity;
    public StackView[] Producers => _producers;
    public StackView[] Consumers => _consumers;
    public StackView AssistantStack => _assistantStack;
    public List<StackPresenter> ProducerItemCreators => _producerItemCreators;
    public List<StackPresenter> ConsumersItemCreators => _consumersItemCreators;
    public Transform TargetTransform => _targetTransform;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _stackPresenter = GetComponent<PlayerStackPresenter>();
        _stateMachine = GetComponent<AssistantStateMachine>();
    }

    private void Start()
    {
        _producers = new StackView[producersContainer.transform.childCount];
        _consumers = new StackView[consumersContainer.transform.childCount];
        //_producerItemCreators = new StackPresenter[producersContainer.transform.childCount];
        //_consumersItemCreators = new StackPresenter[consumersContainer.transform.childCount];
        _assistantStack = GetComponentInChildren<BoxStackView>();

        for (int i = 0; i < _producers.Length; i++)
        {
            _producers[i] = producersContainer.transform.GetChild(i).GetComponentInChildren<StackView>();
            _producerItemCreators.Add(producersContainer.transform.GetChild(i).GetComponent<StackPresenter>());
            //_producerItemCreators[i] = producersContainer.transform.GetChild(i).GetComponent<StackPresenter>();
        }

        for (int i = 0; i < _consumers.Length; i++)
        {
            _consumers[i] = consumersContainer.transform.GetChild(i).GetComponentInChildren<StackView>();
            _consumersItemCreators.Add(consumersContainer.transform.GetChild(i).GetComponent<StackPresenter>());
            //_consumersItemCreators[i] = consumersContainer.transform.GetChild(i).GetComponent<StackPresenter>();
        }
        
        Invoke(nameof(StartStateMachine),3f);
    }

    public void ChangeSpeed(float targetSpeed)
    {
        _speed = targetSpeed;
        _navMeshAgent.speed = targetSpeed;
    }

    public void ChangeCapacity(int target)
    {
        _stackPresenter.ChangeCapacity(target);
    }

    public void ChangeTargetTransform(Transform target)
    {
        _targetTransform = target;
    }

    public void Move(Vector3 target)
    {
        _navMeshAgent.SetDestination(target);
    }

    private void StartStateMachine()
    {
        _stateMachine.enabled = true;
    }
}
