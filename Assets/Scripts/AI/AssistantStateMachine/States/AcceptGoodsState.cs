using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AcceptGoodsState : AssistantState
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AcceptGoods();
    }

    private void AcceptGoods()
    {
        _animator.Play("AcceptGoods");
    }
}
