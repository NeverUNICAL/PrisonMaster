using Agava.IdleGame.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AcceptGoodsState : AssistantState
{
    private Coroutine _coroutineInJob;

    public event UnityAction CapacityFulled;

    private void OnEnable()
    {
        _coroutineInJob = StartCoroutine(CheckStack());
    }

    private void OnDisable()
    {
        if (_coroutineInJob != null)
            StopCoroutine(_coroutineInJob);
    }

    private IEnumerator CheckStack()
    {
        yield return new WaitWhile(() => Assistant.Capacity != Assistant.AssistantStack.StackablesView.Count);

        CapacityFulled?.Invoke();
    }
}
