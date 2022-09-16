using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptGoodsTransition : AssistantTransition
{
    [SerializeField] private AcceptGoodsState _state;

    private void OnEnable()
    {
        _state.CapacityFulled += OnFulled;
    }

    private void OnDisable()
    {
        _state.CapacityFulled -= OnFulled;
    }

    private void OnFulled()
    {
        NeedTransit = true;
    }
}
