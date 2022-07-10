using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;

[RequireComponent(typeof(PlayerStackPresenter))]
public class Assistant : MonoBehaviour
{
    [SerializeField] private ObjectProducerZone _producerTarget;
    [SerializeField] private ObjectConsumerZone _consumerTarget;

    private int _capacity;

    public int Capacity => _capacity;
    public ObjectProducerZone ProducerTarget => _producerTarget;
    public ObjectConsumerZone ConsumerTarget => _consumerTarget;

    private void Awake()
    {
        _capacity = GetComponent<PlayerStackPresenter>().Capacity;
    }
}
