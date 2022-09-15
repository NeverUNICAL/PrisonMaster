using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAssistant : Assistant
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _delay = 3f;

    private void Start()
    {
        StartCoroutine(Delay(_delay));
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Move(_target.position);
    }
}
