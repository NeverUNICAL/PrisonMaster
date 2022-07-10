using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisoner : MonoBehaviour
{
    [SerializeField] private int _reward;
    [SerializeField] private ObjectConsumerZone _consumerTarget;

    public ObjectConsumerZone ConsumerTarget => _consumerTarget;
}
