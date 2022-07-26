using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Distributor : QueueHandler
{
    [Header("Distributor Settings")]
    [SerializeField] private GameObject _exit;
    [SerializeField] private float _tryToSendToExitCooldown;

    private WaitForSeconds _waitToTrySendToExit;

    private void Start()
    {
        _waitToTrySendToExit = new WaitForSeconds(_tryToSendToExitCooldown);
        
        StartCoroutine(TrySendToExit());
        
        if(_queues.Count > 0)
         StartCoroutine(SendToQueue(_queues));
    }
    
    private IEnumerator TrySendToExit()
    {
        while (true)
        {
            if (_prisonerList.Count == _startPoolSize)
            {
                SendToExit();
            }

            yield return _waitToTrySendToExit;
        }
    }

    private void SendToExit()
    {
        _prisonerList[0].SetTarget(_exit,Vector3.zero);
        ExtractFirst();
    }
}
