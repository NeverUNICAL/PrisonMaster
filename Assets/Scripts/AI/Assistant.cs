using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(PlayerStackPresenter))]
public class Assistant : MonoBehaviour
{
    [SerializeField] private ProdusersConteiner _producersConteiner;
    [SerializeField] private ConsumersConteiner _consumersConteiner;

    [Header("OptionsForUpgrade")]
    [SerializeField] private float _speed;

    private NavMeshAgent _navMeshAgent;
    private int _capacity;
    private StackView[] _producers;
    private StackView[] _consumers;
    private StackView _assistantStack;

    private ItemCreator[] _producerItemCreators;
    private StackPresenter[] _consumersItemCreators;
    private Transform _targetTransform;

    public float Speed => _speed;
    public int Capacity => _capacity;
    public StackView[] Producers => _producers;
    public StackView[] Consumers => _consumers;
    public StackView AssistantStack => _assistantStack;
    public ItemCreator[] ProducerItemCreators => _producerItemCreators;
    public StackPresenter[] ConsumersItemCreators => _consumersItemCreators;
    public Transform TargetTransform => _targetTransform;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _capacity = GetComponent<PlayerStackPresenter>().Capacity;
        _producers = new StackView[_producersConteiner.transform.childCount];
        _consumers = new StackView[_consumersConteiner.transform.childCount];
        _producerItemCreators = new ItemCreator[_producersConteiner.transform.childCount];
        _consumersItemCreators = new StackPresenter[_consumersConteiner.transform.childCount];
        _assistantStack = GetComponentInChildren<BoxStackView>();

        for (int i = 0; i < _producers.Length; i++)
        {
            _producers[i] = _producersConteiner.transform.GetChild(i).GetComponentInChildren<StackView>();
            _producerItemCreators[i] = _producersConteiner.transform.GetChild(i).GetComponent<ItemCreator>();
        }

        for (int i = 0; i < _consumers.Length; i++)
        {
            _consumers[i] = _consumersConteiner.transform.GetChild(i).GetComponentInChildren<StackView>();
            _consumersItemCreators[i] = _consumersConteiner.transform.GetChild(i).GetComponent<StackPresenter>();
        }
    }

    private void ChangeSpeed(float targetSpeed)
    {
        _speed = targetSpeed;
    }

    public void ChangeTargetTransform(Transform target)
    {
        _targetTransform = target;
    }

    public void Move(Vector3 target)
    {
        _navMeshAgent.SetDestination(target);
    }
}
