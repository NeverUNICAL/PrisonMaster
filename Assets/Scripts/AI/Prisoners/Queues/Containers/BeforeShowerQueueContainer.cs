using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Agava.IdleGame;
using Agava.IdleGame.Model;
using UnityEngine;
public class BeforeShowerQueueContainer : QueueHandler
{
    [Header("BeforeShowerSettings")]
    [SerializeField] private List<ShowerQueueContainer> _listForShower;

    private void Start()
    {
        GenerateList();
        StartCoroutine(TryToSendPrisoner());
    }
    
    private IEnumerator TryToSendPrisoner()
    {
        while (true)
        {
            if (_prisonerList.Count > 0 && _store.gameObject.activeInHierarchy)
            {
                SendToShower();
            }

            yield return _waitForSendTimeOut;
        }
    }

    private void SendToShower()
    {
        if (_listForShower[0].PrisonerQueueList.Count < _listForShower[0].PoolSize)
        {
            if (_prisonerList.Count > 0)
            {
                _listForShower[0].PrisonerQueueList.Add(_prisonerList[0]);
                _listForShower[0].ListSort();
                ExtractFirst();
            }
        }
    }
}
