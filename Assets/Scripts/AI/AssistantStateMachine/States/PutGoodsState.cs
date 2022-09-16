using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PutGoodsState : AssistantState
{
    private Coroutine _coroutineInJob;

    public event UnityAction CapacityEmpty;

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
        yield return new WaitWhile(() => Assistant.AssistantStack.Stackables.Count != 0);

        CapacityEmpty?.Invoke();
    }
}
