using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingAssistent : Assistant
{
    [SerializeField] private ProdusersContainer producersContainer;
    [SerializeField] private ConsumersContainer consumersContainer;
    [SerializeField] private float _delay;
    [SerializeField] private AssistantBuyZone _buyZone;

    private StackView[] _producers;
    private StackView[] _consumers;
    private StackView _assistantStack;
    private List<StackPresenter> _producerItemCreators = new List<StackPresenter>();
    private List<StackPresenter> _consumersItemCreators = new List<StackPresenter>();

    public StackView[] Producers => _producers;
    public StackView[] Consumers => _consumers;
    public StackView AssistantStack => _assistantStack;
    public List<StackPresenter> ProducerItemCreators => _producerItemCreators;
    public List<StackPresenter> ConsumersItemCreators => _consumersItemCreators;

    private void OnEnable()
    {
        _buyZone.Unlocked += OnUnlocked;
    }

    private void OnDisable()
    {
        _buyZone.Unlocked -= OnUnlocked;
    }

    private void Start()
    {
        _producers = new StackView[producersContainer.transform.childCount];
        _consumers = new StackView[consumersContainer.transform.childCount];
        _assistantStack = GetComponentInChildren<BoxStackView>();

        for (int i = 0; i < _producers.Length; i++)
        {
            _producers[i] = producersContainer.transform.GetChild(i).GetComponentInChildren<StackView>();
            _producerItemCreators.Add(producersContainer.transform.GetChild(i).GetComponent<StackPresenter>());
        }

        for (int i = 0; i < _consumers.Length; i++)
        {
            _consumers[i] = consumersContainer.transform.GetChild(i).GetComponentInChildren<StackView>();
            _consumersItemCreators.Add(consumersContainer.transform.GetChild(i).GetComponent<StackPresenter>());
        }
    }

    private void OnUnlocked(BuyZonePresenter buyZone)
    {
        StartCoroutine(DelayStartStateMachine(_delay));
    }

    private IEnumerator DelayStartStateMachine(float delay)
    {
        yield return new WaitForSeconds(delay);

        StartStateMachine();
    }
}
