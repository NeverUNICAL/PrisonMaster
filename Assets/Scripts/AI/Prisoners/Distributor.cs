using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Distributor : QueueHandler
{
    [SerializeField] private bool _isLastPool = false;

    private void Start()
    {
        if (_queues.Count > 0)
        {
            StartCoroutine(TrySendToExit());
            StartCoroutine(SendToQueue(_queues));
        }
    }

    private IEnumerator TrySendToExit()
    {
        while (true)
        {
            if (_isLastPool)
            {
                if (_prisonerList.Count == _startPoolSize)
                {
                    SendToExit();
                }
            }

            yield return _waitToTrySendToExit;
        }
    }
}
