using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PutGoodsState : AssistantState
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Assistant.Move(Assistant.transform.position);
    }

    private void Update()
    {
        PutGoods();
    }

    private void PutGoods()
    {
        _animator.Play("Idle");
    }
}
