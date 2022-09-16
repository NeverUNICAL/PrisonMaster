using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceConsumerTransition : AssistantTransition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private MoveToConsumerState _currentState;

    private void Update()
    {
        if (Assistant.TargetTransform != null)
        {
            if (Vector3.Distance(transform.position, _currentState.TargetPosition) < _transitionRange)
            {
                _currentState.StopCoroutine();
                NeedTransit = true;
                Assistant.ChangeTargetTransform(null);
            }
        }
    }
}
