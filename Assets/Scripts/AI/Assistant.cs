using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;

[RequireComponent(typeof(PlayerStackPresenter))]
public class Assistant : MonoBehaviour
{
    [SerializeField] private StackView _producerTarget;
    [SerializeField] private StackView _consumerTarget;

    [Header("OptionsForUpgrade")]
    [SerializeField] private float _speed;

    private int _capacity;

    public float Speed => _speed;
    public int Capacity => _capacity;
    public StackView ProducerTarget => _producerTarget;
    public StackView ConsumerTarget => _consumerTarget;

    private void Awake()
    {
        _capacity = GetComponent<PlayerStackPresenter>().Capacity;
    }

    private void ChangeSpeed(float targetSpeed)
    {
        _speed = targetSpeed;
    }
}
