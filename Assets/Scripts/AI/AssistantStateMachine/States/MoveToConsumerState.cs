using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToConsumerState : AssistantState
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, ConsumerTarget.transform.position, _speed * Time.deltaTime);
        transform.LookAt(ConsumerTarget.transform.position);
    }
}
