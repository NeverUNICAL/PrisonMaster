using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutGoodsTransition : AssistantTransition
{
    [SerializeField] private PutGoodsState _state;

    private void OnEnable()
    {
        _state.CapacityEmpty += OnEmpty;
    }

    private void OnDisable()
    {
        _state.CapacityEmpty -= OnEmpty;
    }

    private void OnEmpty()
    {
        NeedTransit = true;
    }
}
