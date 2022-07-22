using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceProducerTransition : AssistantTransition
{
    [SerializeField] private float _transitionRange;

    private void Update()
    {
        if (Assistant.TargetTransform == null)
        {
            Debug.Log("TargetNull");
        }
        else
        {
            if (Vector3.Distance(transform.position, Assistant.TargetTransform.transform.position) < _transitionRange)
            {
                NeedTransit = true;
            }
        }
    }
}
