using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToProducerState : AssistantState
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, ProducerTarget.transform.position, _speed * Time.deltaTime);
        transform.LookAt(ProducerTarget.transform.position);
    }
}
