using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptGoodsTransition : AssistantTransition
{
    [SerializeField] private float _delay;

    private Coroutine _coroutineInJob;

    private void Update()
    {
        int capacityTarget = Assistant.AssistantStack.GetComponentsInChildren<Transform>().Length - 1;
        
        if (capacityTarget == Assistant.Capacity)
        {
            _coroutineInJob = StartCoroutine(Transit(_delay));
        }
    }

    private IEnumerator Transit(float delay)
    {
        yield return new WaitForSeconds(delay);

        NeedTransit = true;

        StopCoroutine(_coroutineInJob);
    }
}
