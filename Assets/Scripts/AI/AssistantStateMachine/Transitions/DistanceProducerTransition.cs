using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceProducerTransition : AssistantTransition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private MoveToProducerState _currentState;

    private void Update()
    {
        if (Assistant.TargetTransform != null)
        {
            if (Vector3.Distance(transform.position, Assistant.TargetTransform.transform.position) < _transitionRange)
            {
                _currentState.StopCoroutine();
                NeedTransit = true;
                Assistant.ChangeTargetTransform(null);
            }
        }
    }
}
