using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutGoods : AssistantTransition
{
    [SerializeField] private float _delay;

    private Coroutine _coroutineInJob;
    private void Update()
    {
        int capacityTarget = Assistant.AssistantStack.GetComponentsInChildren<Transform>().Length - 1;
        if (capacityTarget == 0)
        {
            _coroutineInJob = StartCoroutine(Transit(_delay));
        }
        else
        {
            _coroutineInJob = StartCoroutine(AlternativeTransit(_delay));
        }
    }

    private IEnumerator Transit(float delay)
    {
        yield return new WaitForSeconds(delay);

        NeedTransit = true;

        StopCoroutine(_coroutineInJob);
    }

    private IEnumerator AlternativeTransit(float delay)
    {
        yield return new WaitForSeconds(delay);

        NeedAlternativeTransit = true;

        StopCoroutine(_coroutineInJob);
    }
}
