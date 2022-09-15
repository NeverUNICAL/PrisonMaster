using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Agava.IdleGame;
using Agava.IdleGame.Model;
using UnityEngine;

public class QueueContainer : QueueHandler
{
    [SerializeField] private Distributor _distributor;
    
    private bool _isAngryPrisoner;
    
    private void Start()
    {
        GenerateList();
        StartCoroutine(TryToSendPrisoner());
    }
    
    private IEnumerator TryToSendPrisoner()
    {
        while (true)
        {
            if (_prisonerList.Count > 0 && _shop.gameObject.activeInHierarchy && _prisonerList[0].PathEnded())
            {
                if (_shop.Count >= _shop.CountForSale)
                {
                    if (SendToPool(_distributor)) 
                        _shop.Sale();

                    ChangeAnimationPrisoners(false);
                }
                else
                {
                    ChangeAnimationPrisoners(true);
                }
            }
            
            yield return _waitForSendTimeOut;
        }
    }

    private void ChangeAnimationPrisoners(bool value)
    {
        foreach (var prisoner in _prisonerList)
        {
            if (prisoner.PathEnded())
                prisoner.ChangeAngryAnimation(value);
        }
    }
}
