using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceProducerTransition : AssistantTransition
{
    [SerializeField] private float _transitionRange;

    private void Update()
    {
        if (Vector3.Distance(transform.position, ProducerTarget.transform.position) < _transitionRange)
        {
            NeedTransit = true;
        }
    }
}
