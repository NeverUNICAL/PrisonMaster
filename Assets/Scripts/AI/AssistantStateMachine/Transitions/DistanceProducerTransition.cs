using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceProducerTransition : AssistantTransition
{
    [SerializeField] private float _transitionRange;

    private void Update()
    {
        if (Assistant.TargetTransform != null)
        {
            if (Vector3.Distance(transform.position, Assistant.TargetTransform.transform.position) < _transitionRange)
            {
                Assistant.ChangeTargetTransform(null);
                NeedTransit = true;
            }
        }
    }
}
